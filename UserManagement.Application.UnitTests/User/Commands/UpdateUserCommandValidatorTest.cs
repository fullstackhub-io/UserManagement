using Shouldly;
using System.Linq;
using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.User.Commands;
using Xunit;

namespace UserManagement.ApplicationTests.User.Commands
{
    [Collection("UserCollection")]
    public class UpdateUserCommandValidatorTest
    {
        private readonly IConfigConstants constant;

        public UpdateUserCommandValidatorTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenAllDataIsValid()
        {
            var command = new UpdateUserCommand
            {
                UserID = 100,
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                PhoneNumber = "444-443-4444",
            };

            var validator = new UpdateUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenAllDataIsInValid()
        {
            var command = new UpdateUserCommand{};

            var validator = new UpdateUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLUSERID)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLPHNUM)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLCITY)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLSTATE)).ShouldNotBeNull();
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLCOUNTRY)).ShouldNotBeNull();
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenUserIDIsZero()
        {
            var command = new UpdateUserCommand
            {
                UserID = 0,
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                PhoneNumber = "444-443-4444",
            };

            var validator = new UpdateUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLUSERID)).ShouldNotBeNull();
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenStateIsNull()
        {
            var command = new UpdateUserCommand
            {
                UserID = 100,
                City = "Falls Chruch",
                Country = "USA",
                State = null,
                Zip = "22044",
                PhoneNumber = "444-443-4444",
            };

            var validator = new UpdateUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(e => e.ErrorMessage.Equals(constant.MSG_USER_NULLSTATE)).ShouldNotBeNull();
            result.IsValid.ShouldBeFalse();
        }
    }
}
