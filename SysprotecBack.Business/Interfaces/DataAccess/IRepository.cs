namespace SysprotecBack.Business.Interfaces.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> where TEntity : class
    {
        #region Queries
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        Task<IEnumerable<TEntity>?> GetAllAsync();

        /// <summary>
        /// Gets all where expression asynchronous.
        /// </summary>
        /// <param name="where">The Where Expression.</param>
        /// <param name="includeProperties">The Include Properties.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        Task<IEnumerable<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// Get the first entity found under a specified condition or null otherwise it will find records.
        /// </summary>
        /// <param name="where">The Where.</param>
        /// <param name="includeProperties">The Include Properties.</param>
        /// <returns>TEntity.</returns>
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        /// <summary>
        /// check exist any register asynchronous.
        /// </summary>
        /// <param name="where">The conditional.</param>
        /// <returns>true if contains data, false otherwise.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);
        #endregion

        #region Commands
        /// <summary>
        /// Insert the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>true if created, false otherwise.</returns>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// Insert the entities asynchronous.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>true if created, false otherwise.</returns>
        Task<bool> InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Update the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>true if updated, false otherwise.</returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update the entities asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>true if updated, false otherwise.</returns>
        Task<bool> UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Delete the entity asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>true if deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete the entities asynchronous.
        /// </summary>
        /// <param name="entity">The Entity.</param>
        /// <returns>true if deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Store Procedure
        /// <summary>
        /// Executes a non-query stored procedure asynchronously.
        /// </summary>
        /// <typeparam name="T">The type of result.</typeparam>
        /// <param name="storeProcedure">The name of the stored procedure to execute.</param>
        /// <param name="entity">Optional parameters to pass to the stored procedure.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task<T?> ExecuteStoreProcedureNonQueryAsync<T>(string storeProcedure, T entity);
        #endregion
    }
}