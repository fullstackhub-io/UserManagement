namespace UserManagement.ApplicationTests.User.Commands
{
    using Shouldly;
    using System.Linq;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Application.User.Commands;
    using Xunit;

    [Collection("UserCollection")]
    public class AddUserCommandValidatorTest
    {
        private readonly IConfigConstants constant;

        public AddUserCommandValidatorTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenAllDataIsValid()
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

            var validator = new AddUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenAllDataIsInValid()
        {
            var command = new AddUserCommand
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                City = string.Empty,
                Country = string.Empty,
                State = string.Empty,
                Zip = null,
                EmailAddress = string.Empty,
                Gender = string.Empty,
                PhoneNumber = string.Empty,
            };

            var validator = new AddUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenFirstNameIsEmpty()
        {
            var command = new AddUserCommand
            {
                FirstName = string.Empty,
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

            var validator = new AddUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == constant.MSG_USER_NULLFIRSTNAME).ErrorMessage.ShouldBe(constant.MSG_USER_NULLFIRSTNAME);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenLastNameIsEmpty()
        {
            var command = new AddUserCommand
            {
                FirstName = "John",
                LastName = string.Empty,
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DOB = new System.DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "M",
                PhoneNumber = "444-443-4444",
            };

            var validator = new AddUserCommandValidator(constant);
            var result = validator.Validate(command);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == constant.MSG_USER_NULLLASTNAME).ErrorMessage.ShouldBe(constant.MSG_USER_NULLLASTNAME);
            result.IsValid.ShouldBeFalse();
        }

    }
}
