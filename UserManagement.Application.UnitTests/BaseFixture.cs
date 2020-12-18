namespace UserManagement.ApplicationTests
{
    using AutoMapper;
    using System.Data;
    using UserManagement.Application.Common.Mappings;
    public class BaseFixture
    {
        public IMapper Mapper { get; }


        public IDbConnection DBConnection { get; }

        public BaseFixture()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this.Mapper = configurationProvider.CreateMapper();
        }
    }
}
