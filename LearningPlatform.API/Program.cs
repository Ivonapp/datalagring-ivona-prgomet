using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using LearningPlatform.Application.Services;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Infrastructure.EFC.Repositories;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Infrastructure.EFC.UnitOfWork;
using LearningPlatform.Infrastructure.EFC.Data;
using Microsoft.EntityFrameworkCore;







var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddScoped<ICourseSessionService, CourseSessionService>();
builder.Services.AddScoped<ICourseSessionRepository, CourseSessionRepository>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IUnitOfWork, EfcUnitOfWork>();

builder.Services.AddDbContext<InfrastructureDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer(); //swagger
builder.Services.AddSwaggerGen();           //swagger




var app = builder.Build();
app.UseSwagger();                           //swagger
app.UseSwaggerUI();                         //swagger

app.MapGet("/", () => "API:et är igång!");

app.MapGet("/api/courses", async (ICourseService service) =>
{
    return await service.ListAsync();
});



// app.MapOpenApi(); - kommenterar ut den sålänge då den kan krocka med swagger

app.UseHttpsRedirection();


app.Run();
