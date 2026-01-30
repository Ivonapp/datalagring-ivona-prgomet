using Microsoft.EntityFrameworkCore;
using LearningPlatform.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InfrastructureDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    sql => sql.MigrationsAssembly("LearningPlatform.Infrastructure")
));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
