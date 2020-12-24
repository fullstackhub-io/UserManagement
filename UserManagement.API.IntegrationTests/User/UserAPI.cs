namespace UserManagement.API.IntegrationTests.User
{
    using Xunit;
    using System.Threading.Tasks;
    using Shouldly;
    using UserManagement.Application.User.Commands;
    using System.Net;
    using System;

    public class UserAPI : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public UserAPI(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GivenValidGetAllUserQuery_ReturnSuccessObject()
        {
            await AddNewUser();
            var client = this.factory.GetAnonymousClient();
            var response = await client.GetAsync($"api/User/GetAll");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidGetUserQuery_ReturnSuccessObject()
        {
            var res = await AddNewUser();
            var client = this.factory.GetAnonymousClient();
            var response = await client.GetAsync($"api/User/Get?userID={res}");
            var result = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidAddUserQuery_ReturnSuccessObject()
        {
            var res = await AddNewUser();
            res.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task GivenValidUpdateUserQuery_ReturnSuccessObject()
        {
            var res = await AddNewUser();
            var client = this.factory.GetAnonymousClient();
            var command = new UpdateUserCommand
            {
                UserID = res,
                City = "SpringField",
                Country = "USA",
                State = "VA",
                Zip = "66006",
                PhoneNumber = "888-88-8888",
            };

            var content = IntegrationTestHelper.GetRequestContent(command);
            var response = await client.PutAsync($"api/User/Put", content);
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task GivenValidDeleteUserQuery_ReturnSuccessObject()
        {
            var res = await AddNewUser();
            var client = this.factory.GetAnonymousClient();
            var response = await client.DeleteAsync($"api/User/Delete?userID={res}");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenValidGetUserQuery_SendNullUserID_ReturnErrorCode()
        {
            var client = this.factory.GetAnonymousClient();
            var response = await client.GetAsync($"api/User/Get");
            var result = response.Content.ReadAsStringAsync().Result;
            result.ShouldContain("User ID is required");
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        private async Task<int> AddNewUser()
        {
            var client = this.factory.GetAnonymousClient();
            var command = new AddUserCommand
            {
                FirstName = "Test",
                LastName = "Doe",
                City = "Falls Chruch",
                Country = "USA",
                State = "VA",
                Zip = "22044",
                DOB = new DateTime(1980, 01, 01),
                EmailAddress = "jdoe@fullstackhub.io",
                Gender = "M",
                PhoneNumber = "444-443-4444",
            };

            var content = IntegrationTestHelper.GetRequestContent(command);
            var response = await client.PostAsync($"api/User/Post", content);
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
    }
}