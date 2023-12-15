namespace SysprotecBack.Infrastructure.DataAccess
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using SysprotecBack.Business.Commons.Exceptions;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Infrastructure.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Properties
        protected DbContext DbContext { get; set; }
        protected DbSet<TEntity> Entity { get; set; }
        #endregion

        public Repository(DbContext context)
        {
            DbContext = context;
            DbContext.ChangeTracker.LazyLoadingEnabled = true;
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Entity = DbContext.Set<TEntity>();
        }

        #region Queries
        public async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            var result = await Entity.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            if (where != null) query = query.Where(where);

            var result = await query.AsNoTracking().ToListAsync();
            return result.Any() ? result : null;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> query = Entity.AsQueryable();

            return await query.AsNoTracking().AnyAsync(where);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Entity.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(where);
        }

        #endregion

        #region Commands
        public async Task<bool> InsertAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> InsertAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.AddRangeAsync(entities);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(IEnumerable<TEntity> entities)
        {
            DbContext.Entry(entities).State = EntityState.Modified;

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            DbContext.Remove(entity);

            return await DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(IEnumerable<TEntity> entities)
        {
            DbContext.Remove(entities);

            return await DbContext.SaveChangesAsync() > 0;
        }

        #endregion

        #region Store Procedure
        public async Task<T?> ExecuteStoreProcedureNonQueryAsync<T>(string storeProcedure, T entity)
        {
            try
            {
                var parameters = MapEntityToParameters(entity);
                using var command = CreateCommand(storeProcedure, parameters);
                var result = await command.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? default : (T)result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }
        #endregion

        #region Private Methods
        private StoredProcedureParameters MapEntityToParameters<T>(T entity)
        {
            var parameters = new StoredProcedureParameters();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                parameters.AddParameter(propertyName, propertyValue);
            }

            return parameters;
        }
        
        private DbCommand CreateCommand(string storeProcedure, StoredProcedureParameters parameters)
        {
            try
            {
                var command = DbContext.Database.GetDbConnection().CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = storeProcedure;

                if (parameters != null)
                {
                    foreach (var param in parameters.Parameters)
                    {
                        command.Parameters.Add(new SqlParameter(param.Key, param.Value));
                    }
                }
                OpenConnection();

                return command;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex);
            }
        }

        private void OpenConnection()
        {
            if (DbContext.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                DbContext.Database.GetDbConnection().Open();
            }
        }
        #endregion
    }
}
