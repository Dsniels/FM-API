using System.Reflection.Emit;
using System.Text;
using apisiase.Dto;
using AutoMapper;
using Azure.Storage.Blobs;
using BusinessLogic.Logic;
using BusinessLogic.Persistence;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(b => new BlobServiceClient(blobConnectionString));
            services.AddScoped<IBlobRepository, BlobRepository>();
            // services.AddScoped<IMapper, Mapper>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));
            services.AddTransient<IMateriasRepository, MateriasRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IRentaRepository, RentaReporitory>();
            services.TryAddSingleton<ISystemClock, SystemClock>();




            
            var builder = services.AddIdentityCore<Usuario>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<SeguridadDbContext>();
            builder.AddSignInManager<SignInManager<Usuario>>();





            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                
                options.UseSecurityTokenValidators = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();




            //services.AddScoped(typeof(IGenericSecurityRepository<>), typeof(GenericSecurityRepository<>));
        }
    }
}
