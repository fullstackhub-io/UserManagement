namespace UserManagement.API.IntegrationTests
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;
    using System.Data.SqlClient;
    using System.Net.Http;
    using UserManagement.Application.Common.Interfaces;
    using UserManagement.Domain.UnitOfWork;
    using UserManagement.Persistence.Constant;
    using UserManagement.Persistence.UnitOfWork;

    /// <summary>
    /// The APIs test setup.
    /// </summary>
    /// <typeparam name="TStartup">The input object.</typeparam>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <summary>
        /// Get the anonymous client for testing.
        /// </summary>
        /// <returns>The anonymous client.</returns>
        public HttpClient GetAnonymousClient()
        {
            return this.CreateClient();
        }

        /// <summary>
        /// Setup for middleware.
        /// </summary>
        /// <param name="builder">Take the WebHostBuilder object.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
               .ConfigureServices(services =>
               {
                   services.AddSingleton<IConfigConstants, ConfigConstants>();
                   services.AddSingleton<IDbConnection>(conn => new SqlConnection(conn.GetService<IConfigConstants>().TestFullStackConnection));
                   services.AddTransient<IUnitOfWork>(uof => new UnitOfWork(uof.GetService<IDbConnection>()));
               });
        }
    }
}