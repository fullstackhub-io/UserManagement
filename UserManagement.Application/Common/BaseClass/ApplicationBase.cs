namespace UserManagement.Application.Common.BaseClass
{
    using AutoMapper;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Domain.UnitOfWork;
    public class ApplicationBase
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IConfigConstants ConfigConstants { get; set; }
        public IMapper Mapper { get; set; }

        public ApplicationBase(IConfigConstants configConstants, IUnitOfWork unitOfWork, IMapper mapper)
        {
            ConfigConstants = configConstants;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
