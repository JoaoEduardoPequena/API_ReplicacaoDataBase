using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts;
using Persistence.Repositories;

namespace Persistence
{
    public static class ApplicationPersistenceServiceRegistration
    {
        public static void AddInfraPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dataBasePrimary = configuration.GetConnectionString("PrimaryDatabase");
            var dataBaseReplicas = configuration.GetSection("ConnectionStrings:ReadReplicas").Get<string[]>();

            // Contexto principal (escrita)
            services.AddDbContext<WriteDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("PrimaryDatabase"),
                builderOp => builderOp.MigrationsAssembly(typeof(WriteDbContext).Assembly.FullName)));

            services.AddSingleton(dataBaseReplicas);
            services.AddScoped<ReadReplicaFactory>();
            services.AddScoped<IAppDBContextRouter, AppDBContextRouter>();

            services.AddTransient<IRepoEvento, RepoEvento>();
            services.AddTransient<IRepoPedido, RepoPedido>();
        }
    }
}
