using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Services;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Repositories;
using LearningPlatform.Infrastructure.EFC.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using LearningPlatform.Infrastructure.Extensions;
using LearningPlatform.Application.DTOs;


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
// create - skapa/ta emot info
app.MapPost("/api/teacher", () => { });




// read - hämta info
app.MapGet("/api/teacher", () =>
{
    var list = new List<string>() { "A", "B", "C" }; //Hämta alla lärare?

    return Results.Ok(list);
}); 

// =

app.MapGet("/api/teacher", async (ITeacherService teacherService) =>
{
    var teachers = await teacherService.ListAsync();
    return Results.Ok(teachers);
}); //osäker på om dessa är lika

// = DTO

app.MapGet("/api/teacher", async () =>
{
    var list = new List<TeacherDto>()
    {
        new() { Id = 1, FirstName = "John", LastName = "Svensson", Major = "Matte"},

    };

    return Results.Ok(list);
});






// update - uppdatera info
app.MapPut("/api/teacher", () => { });

// delete - ta bort info
app.MapDelete("/api/teacher", () => { });












app.UseHttpsRedirection();
app.Run();

