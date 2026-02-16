using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.DTOs;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Services;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Repositories;
using LearningPlatform.Infrastructure.EFC.UnitOfWork;
using LearningPlatform.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// CHATGPT - använde chatgpt som ett bollplank och en "lärare" för att förstå
// hur jag skulle t.ex skriva och koppla dom olika service och repo delarna med API:t här inne.
// Samt fick jag hjälp med att ladda ner swagger (jag kanske testar postman sen, men först när jag
// kommit igång med att skriva koden här nedan.)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<ICourseSessionService, CourseSessionService>();
builder.Services.AddScoped<ICourseService, CourseService>();


builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer(); //swagger
builder.Services.AddSwaggerGen();           //swagger
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<InfrastructureDbContext>();
    await db.Database.EnsureCreatedAsync();
}


app.UseSwagger();                           //swagger
app.UseSwaggerUI();                         //swagger
app.UseCors("AllowAll");
// app.MapOpenApi(); - kommenterar ut den sålänge då den kan krocka med swagger





//                  ENDPOINTS
//                  TEACHER


// CREATE - SKAPAR LÄRARE (hans video)
app.MapPost("/api/teachers", (TeacherInput request, ITeacherService service) =>
{
    var input = new TeacherInput(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.Major);
    var teacher = service.CreateAsync(input).Result;

    return Results.Created($"/api/teachers/{teacher}", teacher);
});


// READ - HÄÖMTAR ALLA LÄRARE
app.MapGet("/api/teachers", async (ITeacherService teacherService) =>
{
    var teachers = await teacherService.ListAsync(); //koden för att hämtar alla lärare heter "ListAsync" i ITeacherService.
    return Results.Ok(teachers);
});


// READ - HÄMTAR SPECIFIK LÄRARE
app.MapGet("/api/teachers/{id}", async (int id, ITeacherService teacherService) =>
{
    var teacher = await teacherService.GetByIdAsync(id); // Koden för att hämta en specifik lärare heter "GetByIdAsync" i ITeacherService.

    return teacher is null ? Results.NotFound() : Results.Ok(teacher);
});


// UPDATE - tex ändrar email och sparar den nya ändringen
app.MapPut("/api/teachers/{id}", async (int id, TeacherInput request, ITeacherService service) =>
{
    await service.UpdateAsync(id, request); //ITeracherService
    return Results.NoContent(); 
});


// DELETE - raderar läraren
app.MapDelete("/api/teachers/{id}", async (int id, ITeacherService service) =>
{
    await service.DeleteAsync(id); //ITeracherService
    return Results.NoContent(); // 
});









app.UseHttpsRedirection();
app.Run();

