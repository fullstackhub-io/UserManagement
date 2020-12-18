using Shouldly;
using System.Linq;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.Commands;
using Xunit;

namespace UserManagement.ApplicationTests.User.Commands
{
    [Collection("UserCollection")]
    public class DeleteUserCommandValidatorTest
    {
        private readonly IConfigConstants constant;

        public DeleteUserCommandValidatorTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenAllDataIsValid()
        {
            var command = new DeleteUserCommand
            {
                UserID = 100,
            };

            var validator = new DeleteUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenAllDataIsInValid()
        {
            var command = new DeleteUserCommand
            {
                UserID = 0,
            };

            var validator = new DeleteUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeFalse();
        }

    }
}
