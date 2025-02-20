
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebDating.Data;
using WebDating.Entities;
using WebDating.Extensions;
using WebDating.Interfaces;
using WebDating.Services;
using WebDating.SignalR;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<IDatingService, DatingService>();
builder.Services.AddScoped<IPostService, PostService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("https://localhost:4200", "http://localhost:4200")
);


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<PresenceHub>("hubs/presence");//"hubs/presence" gi? client t? th?y t? trung t? PresenceHub
app.MapHub<MessageHub>("hubs/message");
app.MapHub<CommentSignalR>("hubs/commentHub");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

//seed data
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

    await context.Database.MigrateAsync();
    await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();


