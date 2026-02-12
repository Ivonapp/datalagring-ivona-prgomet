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


// CHATGPT - använde chatgpt som ett bollplank och en "lärare" för att förstå
// hur jag skulle t.ex skriva och koppla dom olika service och repo delarna med API:t här inne.
// Samt fick jag hjälp med att ladda ner swagger (jag kanske testar postman sen, men först när jag
// kommit igång med att skriva koden här nedan.)
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
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));



var app = builder.Build();
app.UseSwagger();                           //swagger
app.UseSwaggerUI();                         //swagger
app.UseCors("AllowAll");





//HÄR SKA VI SKRIVA ALLA CRUDS FÖR ALLA SERVICES

//TEACHER

// CREATE - HÄMTAR HELA LISTAN MED ALLA LÄRARE





// PARTICIPANT
// CREATE - HÄMTAR HELA LISTAN MED ALLA LÄRARE



// ENROLLMENT
// CREATE - HÄMTAR HELA LISTAN MED ALLA LÄRARE



// COURSESESSION
// CREATE - HÄMTAR HELA LISTAN MED ALLA LÄRARE



// COURSE
// CREATE - HÄMTAR HELA LISTAN MED ALLA LÄRARE












// app.MapOpenApi(); - kommenterar ut den sålänge då den kan krocka med swagger

app.UseHttpsRedirection();


app.Run();



// FÖLJ C R U D
// Create – skapa nya poster 
// Read – läsa eller hämta
// Update – uppdatera eller ändra
// Delete – ta bort





//HANS VIDEO

// CREATE
//      app.MapPost("/api/users", () => { });

// READ
//      app.MapGet("/api/users", () => { });

// UPDATE
//      app.MapPut("/api/users", () => { });
//      app.MapPatch("/api/users", () => { });

// DELETE
//      app.MapDelete("/api/users", () => { });







//  return Results.Ok(); // returnerar 200 statuskod (allt har gått bra och jag har utfört det jag ska.)
//  return Results.NoContent(); // returnerar en 204 statuskod, (jag har utför det jag ska göra, men skickar inte tillbaka nån data)
//  return Results.BadRequest(); // returnerar en 400. (när något har gått fel på klient sidan, en användare har matat in FEL information.)
//  return Results.Conflict(); // returnerar en 409 statuskod. (t.ex om vi försöker skapa en användare som REDAN finns.)
//  return Results.InternalServerError(); // returnerar 500 statuskod. (Ej användaren som gjort fel, utan något i serverkommunikationen har gått fel.)