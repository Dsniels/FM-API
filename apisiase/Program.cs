using apisiase;
using apisiase.Middleware;
using BusinessLogic.Data;
using BusinessLogic.Logic;
using BusinessLogic.Persistence;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<SiaseDbContext>();
    await context.Database.MigrateAsync();


    var IdentityContext = service.GetRequiredService<SeguridadDbContext>();


    var userManager = service.GetRequiredService<UserManager<Usuario>>();
    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentityContext.Database.MigrateAsync(); 
    await SecurityDataSeed.SeedUserAsync(userManager,roleManager);

}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/GlobalChat");
app.UseHttpsRedirection();
app.Run();

