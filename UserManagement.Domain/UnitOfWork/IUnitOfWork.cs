namespace UserManagement.Domain.UnitOfWork
{
    using UserManagement.Domain.Repositories;
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        void StartTransaction();
        void Commit();
    }
}
