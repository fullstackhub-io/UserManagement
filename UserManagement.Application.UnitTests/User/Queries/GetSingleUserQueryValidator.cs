namespace UserManagement.ApplicationTests.User.Queries
{
    using Shouldly;
    using System.Linq;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Application.User.Queries;
    using Xunit;

    [Collection("UserCollection")]
    public class GetSingleUserQueryValidatorTest
    {
        private readonly IConfigConstants constant;

        public GetSingleUserQueryValidatorTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
        }

        [Fact]
        public void Validate_ReturnTrue_WhenAllDataIsValid()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 110,
            };

            var validator = new GetSingleUserQueryValidator(constant);
            var result = validator.Validate(query);
            result.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Validate_ReturnFalse_WhenAllDataIsInValid()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 0,
            };

            var validator = new GetSingleUserQueryValidator(constant);
            var result = validator.Validate(query);
            result.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Validate_ReturnErrorMsg_WhenUserIDIsInValid()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 0,
            };

            var validator = new GetSingleUserQueryValidator(constant);
            var result = validator.Validate(query);
            result.Errors.FirstOrDefault(x => x.ErrorMessage == constant.MSG_USER_NULLUSERID).ErrorMessage.ShouldBe(constant.MSG_USER_NULLUSERID);
            result.IsValid.ShouldBeFalse();
        }
    }
}
