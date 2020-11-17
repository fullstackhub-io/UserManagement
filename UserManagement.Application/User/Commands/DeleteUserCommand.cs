namespace UserManagement.Application.User.Commands
{
    using AutoMapper;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.BaseClass;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Domain.UnitOfWork;
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserID { get; set; }
        public class DeleteUserHandler : ApplicationBase, IRequestHandler<DeleteUserCommand, bool>
        {
            public DeleteUserHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                this.UnitOfWork.StartTransaction();
                var res = UnitOfWork.Users.DeleteUser(request.UserID).Result;
                this.UnitOfWork.Commit();
                return await Task.Run(() => res, cancellationToken);
            }
        }
    }
}
