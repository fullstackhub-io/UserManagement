using AutoMapper;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.Commands;
using UserManagement.Domain.UnitOfWork;
using Xunit;

namespace UserManagement.ApplicationTests.User.Commands
{
    [Collection("UserCollection")]
    public class DeleteUserCommandTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DeleteUserCommandTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            unitOfWork = userFixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var command = new DeleteUserCommand
            {
                UserID = 100,
            };

            var handler = new DeleteUserCommand.DeleteUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBeOfType<bool>();
        }

        [Fact]
        public async Task Handle_ReturnTrue_WhenSendCorrectUserIDIsSent()
        {
            var command = new DeleteUserCommand
            {
                UserID = 100,
            };

            var handler = new DeleteUserCommand.DeleteUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(command, CancellationToken.None);
            result.ShouldBe(true);
        }
    }
}
