using System.Text;
using Azure.Storage.Blobs;
using BusinessLogic.Logic;
using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace apisiase
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var IdentiryConnectionString = configuration.GetConnectionString("SecurityConnection");
            var blobConnectionString = configuration.GetConnectionString("BlobConnection");



            services.AddDbContext<SiaseDbContext>(opt => opt.UseSqlServer(connectionString));
            services.AddDbContext<SeguridadDbContext>(opt => opt.UseSqlServer(IdentiryConnectionString));
            services.AddProblemDetails();
            services.AddScoped(b => new BlobServiceClient(blobConnectionString));
            services.AddScoped<IBlobRepository, BlobRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));
            services.AddTransient<IMateriasRepository, MateriasRepository>();
            services.AddTransient<IRentaRepository, RentaReporitory>();




            var builder = services.AddIdentityCore<Usuario>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<SeguridadDbContext>();
            builder.AddSignInManager<SignInManager<Usuario>>();





            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });




            //services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));
        }
    }
}
