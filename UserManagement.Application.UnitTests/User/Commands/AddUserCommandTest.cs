namespace UserManagement.ApplicationTests.User.Commands
{
    using AutoMapper;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Application.User.Commands;
    using UserManagement.Domain.UnitOfWork;
    using Xunit;

    [Collection("UserCollection")]
    public class AddUserCommandTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AddUserCommandTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            unitOfWork = userFixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var command = new AddUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DOB = new System.DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "M",
                PhoneNumber = "444-443-4444",
            };

            var handler = new AddUserCommand.AddNewUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBeOfType<int>();
        }

        [Fact]
        public async Task Handle_ReturnCorrectUserID_WhenSendCorrectPayload()
        {
            var command = new AddUserCommand
            {
                FirstName = "John",
                LastName = "Doe",
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DOB = new System.DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "M",
                PhoneNumber = "444-443-4444",
            };

            var handler = new AddUserCommand.AddNewUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(100);
        }
    }
}
