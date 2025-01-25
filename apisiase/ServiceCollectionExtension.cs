using Azure.Storage.Blobs;
using BusinessLogic.Logic;
using BusinessLogic.Persistence;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace apisiase
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var blobConnectionString = configuration.GetValue<string>("BlobConnection");
            System.Console.WriteLine(connectionString);
            services.AddDbContext<SiaseDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddProblemDetails();
            services.AddScoped(b=> new BlobServiceClient(blobConnectionString));
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IMateriasRepository, MateriasRepository>();
            //services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));
        }
    }
}
