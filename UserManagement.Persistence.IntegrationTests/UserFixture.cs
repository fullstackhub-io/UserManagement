namespace UserManagement.Persistance.IntegrationTests
{
    using System.Data;
    using System.Data.SqlClient;
    using UserManagement.Domain.Repositories;
    using UserManagement.Persistence.Repositories;
    using Xunit;
    public class UserFixture
    {
        public IUserRepository UserRepository { get; }

        public UserFixture()
        {
            IDbConnection dbConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Persist Security Info=True;Integrated Security=SSPI;Initial Catalog=TestFullstackHub");
            UserRepository = new UserRepository(dbConnection, null);
        }

        [CollectionDefinition("UserCollection")]
        public class QueryCollection : ICollectionFixture<UserFixture>
        {
        }
    }
}
