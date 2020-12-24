namespace UserManagement.ApplicationTests.User.Queries
{
    using AutoMapper;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Application.User.Queries;
    using UserManagement.Application.User.VM;
    using UserManagement.Domain.UnitOfWork;
    using Xunit;

    [Collection("UserCollection")]
    public class GetSingleUserQueryTest
    {
        private readonly IConfigConstants constant;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetSingleUserQueryTest(UserFixture userFixture)
        {
            constant = userFixture.Constant;
            mapper = userFixture.Mapper;
            unitOfWork = userFixture.UnitOfWork;
        }

        [Fact]
        public async Task Handle_ReturnsCorrectVM()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 110,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.ShouldBeOfType<UserVM>();
        }

        [Fact]
        public async Task Handle_ReturnCorrectAge_WhenDOBIsProvided()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 110,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.UserList[0].Age.ShouldBe(21);
        }

        [Fact]
        public async Task Handle_ReturnCorrectsalutation_WhenGenderIsProvided()
        {
            var query = new GetSingleUserQuery
            {
                UserID = 100,
            };

            var handler = new GetSingleUserQuery.GetSingleUserHandler(constant, mapper, unitOfWork);
            var result = await handler.Handle(query, CancellationToken.None);
            result.UserList[0].Salutation.ShouldContain("Sir");
        }
    }
}
