namespace UserManagement.Persistance.IntegrationTests.User
{
    using Shouldly;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using UserManagement.Domain.Repositories;
    using Xunit;

    [Collection("UserCollection")]
    public class UserRepositoryTest : IDisposable
    {
        private readonly IUserRepository userRepository;
        public UserRepositoryTest(UserFixture fixture)
        {
            userRepository = fixture.UserRepository;
        }

        [Fact]
        public async Task TestAddUser_GivenCorrectParam_ReturnUserID()
        {
            var res = await AddNewUser();
            res.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestGetAllUsers_GivenCorrectParam_ReturnUserList()
        {
            await AddNewUser();
            var res = await userRepository.GetAllUsers();
            res.Count().ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task TestGetUserByID_GivenCorrectParam_ReturnUserList()
        {
            var userId = await AddNewUser();
            var res = await userRepository.GetUser(userId);
            res.ShouldBeOfType<Domain.Entities.User>();
        }


        [Fact]
        public async Task TestUpdateUser_GivenCorrectParam_ReturnTrue()
        {
            var userId = AddNewUser().Result;
            var user = new Domain.Entities.User
            {
                FirstName = "John",
                LastName = "Doe",
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DateAdded = new DateTime(2019, 01, 01),
                DOB = new DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "F",
                PhoneNumber = "000-000-000",
                UserID = userId,
            };

            var res = await userRepository.UpdateUser(user);
            res.ShouldBeTrue();
        }

        [Fact]
        public async Task TestDeleteUser_GivenCorrectParam_ReturnTrue()
        {
            var userId = AddNewUser().Result;
            var res = await userRepository.DeleteUser(userId);
            res.ShouldBeTrue();
        }

        private async Task<int> AddNewUser()
        {
            var user = new Domain.Entities.User
            {
                FirstName = "John",
                LastName = "Doe",
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DateAdded = new DateTime(2019, 01, 01),
                DOB = new DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "M",
                PhoneNumber = "444-443-4444"
            };

            return await userRepository.AddUser(user);
        }

        public void Dispose()
        {
            userRepository.DeleteAllUser();
        }
    }
}