namespace SysprotecBack.Business.Interfaces.DataAccess
{
    using System.Threading.Tasks;
    using SysprotecBack.Core.Entities;

    public interface IUnitOfWork
    {
        #region Transactions
        /// <summary>
        /// starts a new transaction asynchronous.
        /// </summary>
        public Task BeginTransactionAsync();

        /// <summary>
        /// Commits all changes made to the database in the current transaction asynchronously.
        /// </summary>
        public Task CommitTransactionAsync();

        /// <summary>
        /// releasing, or resetting unmanaged resources asynchronously.
        /// </summary>
        public Task CloseTransactionAsync();

        /// <summary>
        /// Discards all changes made to the database in the current transaction asynchronously.
        /// </summary>
        public Task RollbackTransactionAsync();
        #endregion

        #region Repositories
        IRepository<Role> Role { get; }
        IRepository<User> User { get; }
        IRepository<Status> Status { get; }
        IRepository<Service> Service { get; }
        IRepository<Report> Report { get; }
        IRepository<UserRole> UserRole { get; }
        #endregion
    }
}
