namespace SysprotecBack.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using SysprotecBack.Business.Interfaces.DataAccess;
    using SysprotecBack.Core.Entities;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private DbContext DbContext { get; set; }
        private IDbContextTransaction? _transaction;
        private IRepository<Role>? _role;
        private IRepository<User>? _user;
        private IRepository<Status>? _status;
        private IRepository<Service>? _service;
        private IRepository<Report>? _report;
        private IRepository<UserRole>? _userRole;
        #endregion

        public UnitOfWork(SysprotecContext dbContext)
        {
            DbContext = dbContext;
        }

        #region Transactions
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await SaveAsync();
            }
        }

        public async Task CloseTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
        #endregion

        #region Repositories
        public IRepository<Role> Role
        {
            get
            {
                return _role ??= new Repository<Role>(DbContext);
            }
        }
        public IRepository<User> User
        {
            get
            {
                return _user ??= new Repository<User>(DbContext);
            }
        }

        public IRepository<Status> Status
        {
            get
            {
                return _status ??= new Repository<Status>(DbContext);
            }
        }

        public IRepository<Service> Service
        {
            get
            {
                return _service ??= new Repository<Service>(DbContext);
            }
        }

        public IRepository<Report> Report
        {
            get
            {
                return _report ??= new Repository<Report>(DbContext);
            }
        }

        public IRepository<UserRole> UserRole
        {
            get
            {
                return _userRole ??= new Repository<UserRole>(DbContext);
            }
        }
        #endregion

        #region Private Methods
        private async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
        #endregion
    }
}
