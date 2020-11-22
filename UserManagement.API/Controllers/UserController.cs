namespace UserManagement.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UserManagement.Application.User.Commands;
    using UserManagement.Application.User.Queries;
    using UserManagement.Application.User.VM;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet("[action]")]
        public async Task<ActionResult<UserVM>> Get(int userID)
        {
            return await this.Mediator.Send(new GetSingleUserQuery { UserID = userID });
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<UserVM>> GetAll()
        {
            return await this.Mediator.Send(new GetAllUserQuery());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> Post(AddUserCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<bool>> Put(UpdateUserCommand command)
        {
            return await this.Mediator.Send(command);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult<bool>> Delete(int userID)
        {
            return await this.Mediator.Send(new DeleteUserCommand { UserID = userID });
        }
    }
}