using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Repositories;
using LearningPlatform.Infrastructure.EFC.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        // Hämta din nya connectionstring från appsettings.json
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Vi skippar "if (env.IsDevelopment())" för SQLite helt. 
        // Nu kör vi SQL Server oavsett miljö.
        services.AddDbContext<InfrastructureDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Dina repositories
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

        services.AddScoped<IUnitOfWork, EfcUnitOfWork>();

        return services;
    }
}