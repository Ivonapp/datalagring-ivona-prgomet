using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.DTOs;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
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



//  IF raden nedan krockar med mina Migrations, därav kommenterad ut. Kan nog radera den helt men låter den va for now
/*if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<InfrastructureDbContext>();
    await db.Database.EnsureCreatedAsync();
}*/







app.UseSwagger();                           //swagger
app.UseSwaggerUI();                         //swagger
app.UseCors("AllowAll");
// app.MapOpenApi(); - kommenterar ut den sålänge då den kan krocka med swagger





//                  ENDPOINTS
//                  TEACHER
    // CREATE - Skapar lärare
    app.MapPost("/api/teachers", async (TeacherInput request, ITeacherService service) =>
    {
        try
        {
            var teacherId = await service.CreateAsync(request);
            return Results.Created($"/api/teachers/{teacherId}", new { Id = teacherId });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    });

    // READ - Hämta alla lärare
    app.MapGet("/api/teachers", async (ITeacherService service) =>
    {
        var teachers = await service.ListAsync();
        return Results.Ok(teachers);
    });

    // READ - Hämta specifik lärare
    app.MapGet("/api/teachers/{id}", async (int id, ITeacherService service) =>
    {
        var teacher = await service.GetByIdAsync(id);
        return teacher is null ? Results.NotFound() : Results.Ok(teacher);
    });

    // UPDATE - Ändra lärare
    app.MapPut("/api/teachers/{id}", async (int id, TeacherInput request, ITeacherService service) =>
    {
        try
        {
            await service.UpdateAsync(id, request);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });

    // DELETE - Radera lärare
    app.MapDelete("/api/teachers/{id}", async (int id, ITeacherService service) =>
    {
        try
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });







//                  PARTICIPANT

    // CREATE - Skapar participant
    app.MapPost("/api/participants", async (ParticipantInput request, IParticipantService service) =>
    {
        try
        {
            var participantId = await service.CreateAsync(request);
            return Results.Created($"/api/participants/{participantId}", new { Id = participantId });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    });

    // READ - Hämta alla deltagare
    app.MapGet("/api/participants", async (IParticipantService service) =>
    {
        var participants = await service.ListAsync();
        return Results.Ok(participants);
    });

    // READ - Hämta specifik deltagare
    app.MapGet("/api/participants/{id}", async (int id, IParticipantService service) =>
    {
        var participant = await service.GetByIdAsync(id);
        return participant is null ? Results.NotFound() : Results.Ok(participant);
    });

    // UPDATE - Ändra participant
    app.MapPut("/api/participants/{id}", async (int id, ParticipantInput request, IParticipantService service) =>
    {
        try
        {
            await service.UpdateAsync(id, request);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });

    // DELETE - Radera participant
    app.MapDelete("/api/participants/{id}", async (int id, IParticipantService service) =>
    {
        try
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });




//                 COURSE


    // CREATE - Skapa kurs
    app.MapPost("/api/courses", async (CourseInput request, ICourseService service) =>
    {
        try
        {
            var courseId = await service.CreateAsync(request);
            return Results.Created($"/api/courses/{courseId}", new { Id = courseId });
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    });

    // READ - Hämta alla kurser
    app.MapGet("/api/courses", async (ICourseService service) =>
    {
        var courses = await service.ListAsync();
        return Results.Ok(courses);
    });

    // UPDATE - Ändra kurs
    app.MapPut("/api/courses/{id}", async (int id, CourseInput request, ICourseService service) =>
    {
        try
        {
            await service.UpdateAsync(id, request);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });

    // DELETE - Radera kurs
    app.MapDelete("/api/courses/{id}", async (int id, ICourseService service) =>
    {
        try
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.NotFound(new { error = ex.Message });
        }
    });












//                  COURSE SESSION      

// CREATE
app.MapPost("/api/coursesessions", async (CourseSessionInput request, ICourseSessionService service) =>
{
    try
    {
        var sessionId = await service.CreateAsync(request);
        return Results.Created($"/api/coursesessions/{sessionId}", new { Id = sessionId });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

// READ - Hämta alla kurs-sessioner
app.MapGet("/api/coursesessions", async (ICourseSessionService service) =>
{
    var sessions = await service.ListAsync();
    return Results.Ok(sessions);
});

// UPDATE - Ändra kurs-session
app.MapPut("/api/coursesessions/{id}", async (int id, CourseSessionInput request, ICourseSessionService service) =>
{
    try
    {
        await service.UpdateAsync(id, request);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

// DELETE - Radera kurs-session
app.MapDelete("/api/coursesessions/{id}", async (int id, ICourseSessionService service) =>
{
    try
    {
        await service.DeleteAsync(id);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});








//              NEDAN KOD PÅGÅENDE 
//                  ENROLLMENT

// CREATE - Ansökan till kurs
app.MapPost("/api/enrollments", async (EnrollmentInput request, IEnrollmentService service) =>
{
    try
    {
        var enrollmentId = await service.CreateAsync(request);
        return Results.Created($"/api/enrollments/{enrollmentId}", new { Id = enrollmentId });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

// READ - Alla ansökningar
app.MapGet("/api/enrollments", async (IEnrollmentService service) =>
{
    var enrollments = await service.ListAsync();
    return Results.Ok(enrollments);
});

// UPDATE - Ändra ansökan
app.MapPut("/api/enrollments/{id}", async (int id, EnrollmentInput request, IEnrollmentService service) =>
{
    try
    {
        await service.UpdateAsync(id, request);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});

// DELETE - Ta bort ansökan
app.MapDelete("/api/enrollments/{id}", async (int id, IEnrollmentService service) =>
{
    try
    {
        await service.DeleteAsync(id);
        return Results.NoContent();
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(new { error = ex.Message });
    }
});










app.UseHttpsRedirection();
app.Run();

