namespace UserManagement.Application.User.Queries
{
    using AutoMapper;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.BaseClass;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Application.User.DTO;
    using UserManagement.Application.User.VM;
    using UserManagement.Domain.UnitOfWork;
    public class GetAllUserQuery : IRequest<UserVM>
    {
        public class GetAllUserHandler : ApplicationBase, IRequestHandler<GetAllUserQuery, UserVM>
        {
            public GetAllUserHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<UserVM> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var res = Mapper.Map(UnitOfWork.Users.GetAllUsers().Result, new List<UserDTO>());
                return await Task.FromResult(new UserVM() { UserList = res });
            }
        }
    }
}
