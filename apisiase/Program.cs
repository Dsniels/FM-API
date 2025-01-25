using apisiase;
using apisiase.Middleware;
using BusinessLogic.Logic;
using BusinessLogic.Persistence;
using Core.Entities;
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
    var context = scope.ServiceProvider.GetRequiredService<SiaseDbContext>();
    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseRouting();
app.MapHub<ChatHub>("/GlobalChat");
app.UseHttpsRedirection();
app.Run();

