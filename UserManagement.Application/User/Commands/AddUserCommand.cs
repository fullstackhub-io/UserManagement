namespace UserManagement.Application.User.Commands
{
    using AutoMapper;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using UserManagement.Application.Common.BaseClass;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Domain.UnitOfWork;
    public class AddUserCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public class AddNewUserHandler : ApplicationBase, IRequestHandler<AddUserCommand, int>
        {
            public AddNewUserHandler(IConfigConstants constant, IMapper mapper, IUnitOfWork unitOfWork)
                : base(constant, unitOfWork, mapper)
            {
            }

            public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                var user = new UserManagement.Domain.Entities.User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    DOB = request.DOB,
                    Gender = request.Gender.ToUpper(),
                    EmailAddress = request.EmailAddress,
                    PhoneNumber = request.PhoneNumber,
                    City = request.City,
                    State = request.State,
                    Zip = request.Zip,
                    Country = request.Country
                };
                this.UnitOfWork.StartTransaction();
                var res = UnitOfWork.Users.AddUser(user).Result;
                this.UnitOfWork.Commit();
                return await Task.Run(() => res, cancellationToken);
            }
        }
    }
}
