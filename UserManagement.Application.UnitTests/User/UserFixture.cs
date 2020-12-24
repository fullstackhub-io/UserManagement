namespace UserManagement.ApplicationTests.User
{
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Domain.Repositories;
    using UserManagement.Domain.UnitOfWork;
    using Xunit;
    public class UserFixture : BaseFixture
    {
        public IConfigConstants Constant { get; }
        public IUnitOfWork UnitOfWork { get; }

        public UserFixture()
        {
            var mockConstant = new Mock<IConfigConstants>();
            mockConstant.SetupGet(p => p.MSG_USER_NULLUSERID).Returns("User Name is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLFIRSTNAME).Returns("First Name is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLLASTNAME).Returns("Last Name is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLDOB).Returns("DOB is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLGENDER).Returns("Gender is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLEMAILADDR).Returns("Email is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLPHNUM).Returns("Phone Number is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLCITY).Returns("City is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLSTATE).Returns("State is required!");
            mockConstant.SetupGet(p => p.MSG_USER_NULLCOUNTRY).Returns("Country is required!");
            Constant = mockConstant.Object;

            var mockUserRepo = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            MockAddUser(mockUserRepo);
            MockUpdateUser(mockUserRepo);
            MockDeleteUser(mockUserRepo);
            MockGetAllUser(mockUserRepo);
            MockGetUser(mockUserRepo);
            mockUnitOfWork.SetupGet(repo => repo.Users).Returns(mockUserRepo.Object);
            UnitOfWork = mockUnitOfWork.Object;
        }

        private Mock<IUserRepository> MockAddUser(Mock<IUserRepository> mockRepo)
        {
            mockRepo.Setup(p => p.AddUser(It.IsAny<UserManagement.Domain.Entities.User>())).Returns(Task.Run(() => 100));
            return mockRepo;
        }

        private Mock<IUserRepository> MockUpdateUser(Mock<IUserRepository> mockRepo)
        {
            mockRepo.Setup(p => p.UpdateUser(It.IsAny<UserManagement.Domain.Entities.User>())).Returns(Task.Run(() => true));
            return mockRepo;
        }

        private Mock<IUserRepository> MockDeleteUser(Mock<IUserRepository> mockRepo)
        {
            mockRepo.Setup(p => p.DeleteUser(100)).Returns(Task.Run(() => true));
            return mockRepo;
        }

        private Mock<IUserRepository> MockGetAllUser(Mock<IUserRepository> mockRepo)
        {
            mockRepo.Setup(p => p.GetAllUsers()).Returns(Task.Run(() => GetUserList()));
            return mockRepo;
        }

        private Mock<IUserRepository> MockGetUser(Mock<IUserRepository> mockRepo)
        {

            mockRepo.Setup(p => p.GetUser(110)).Returns(Task.Run(() => GetUserList().FirstOrDefault(u => u.UserID == 110)));
            mockRepo.Setup(p => p.GetUser(100)).Returns(Task.Run(() => GetUserList().FirstOrDefault(u => u.UserID == 100)));
            return mockRepo;
        }

        private IEnumerable<Domain.Entities.User> GetUserList()
        {
            return new List<Domain.Entities.User>
            {
                new Domain.Entities.User
                {
                    FirstName = "John",
                    LastName = "Doe",
                    City = "Falls Chruch",
                    Country = "USA",
                    State = "VA",
                    Zip = "22044",
                    DateAdded = new System.DateTime(2019,01,01),
                    DOB = new System.DateTime(1980,01,01),
                    EmailAddress = "jdoe@fullstackhub.io",
                    Gender = "M",
                    PhoneNumber = "444-443-4444",
                    UserID = 100
                },
                 new Domain.Entities.User
                {
                    FirstName = "Lina",
                    LastName = "Smith",
                    City = "Fairfax",
                    Country = "USA",
                    State = "VA",
                    Zip = "22019",
                    DateAdded = new System.DateTime(2012,01,01),
                    DOB = new System.DateTime(1999,01,01),
                    EmailAddress = "lsmith@fullstackhub.io",
                    Gender = "F",
                    PhoneNumber = "333-443-7777",
                    UserID = 110
                }
            };
        }
        [CollectionDefinition("UserCollection")]
        public class QueryCollection : ICollectionFixture<UserFixture>
        {
        }
    }
}