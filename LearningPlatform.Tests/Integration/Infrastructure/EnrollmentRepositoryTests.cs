using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Integration;
using Xunit;

namespace LearningPlatform.Tests.Integration.Infrastructure;

[Collection(SqliteInMemoryCollection.Name)]

/*                                          *** CHATGPT ***
    
    Jag använde chatgpt som hjälp/bollplank för ALLA klasserna i Integrations tester, men också som ett sätt
    att kunna se och förstå HUR strukturen och koden i integrationstester ser ut i jämförelse med enhetstester. 
    Det tog väldigt lång tid att skriva klart Integrationstesterna (tillskillnad från Enhetstesterna) då enhetsterter
    är mycket enklare att förstå samt mycket enklare kod.
    
    När jag första gången hade skrivit klart alla integrationstester, och försökte köra testet, fick jag buggar och error
    i alla klasser HELA tiden, och hur jag än gjorde, ändrade etc så fortsatte jag få buggar. Tillslut fick jag börja
    om där jag fick radera alla integrationstester jag skrivit och startade om.
    
    ChatGPT hjälpte mig att SE strukturen och HUR integrationstester skrivs, byggs och bara all-in-all
    hur man ska skriva dom olika delarna. Hur dom olika CRUD och AAA delarna ser ut, Hur man t.ex. ska göra med klasser
    som har olika beroenden såsom Enrollment som har koppling till andra klasser.
    
    Även då denna delen var otroligt svår och jag fick börja om helt, så var det på ett sätt just omstarten som faktiskt
    hjälpte mig att "nöta in" koden.
    
*/


public sealed class EnrollmentRepositoryTests(SqliteInMemoryFixture fixture)
{

    //                                      CREATE
    [Fact]

    
    public async Task Add_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        // UNIKA BEROENDEN FÖR ENROLLMENT


        var course = new CourseEntity {                         // COURSE
            Title = "AddTitle", 
            CourseCode = 101, 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);                                 // COURSESESSION
        var session = new CourseSessionEntity { 
            Course = course,                                    // ??
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);                         // PARTICIPANT
        var participant = new ParticipantEntity { 
            FirstName = "Firstname", 
            LastName = "Lastname", 
            Email = "enrollmentcreate@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(participant);
        await db.SaveChangesAsync();

        var model = new EnrollmentModel(
            0, 
            new byte[8], 
            DateTime.UtcNow, 
            null, participant.Id, 
            session.Id
            );


        //                          Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        Assert.True(await db.Enrollments.AnyAsync(x => x.ParticipantId == participant.Id));
    }










    //                                      READ
    [Fact]
    public async Task Get_ShouldWork()
    {

        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        var course = new CourseEntity { 
            Title = "GetTitle", 
            CourseCode = 102, 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        var coursesession = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] };

        db.CourseSessions.Add(coursesession);
        var participant = new ParticipantEntity { 
            FirstName = "Firstname", 
            LastName = "Lastname", 
            Email = "enrollmentread@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(participant);

        var enrollment = new EnrollmentEntity { 
            Participant = participant, 
            CourseSession = coursesession, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enrollment);
        await db.SaveChangesAsync();

        //                          Act
        var result = await repo.GetByIdAsync(enrollment.Id);

        //                          Assert
        Assert.NotNull(result);
    }




    //                                      UPDATE
    [Fact]
    public async Task Update_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        var course = new CourseEntity { 
            Title = "UpdateTitle", 
            CourseCode = 103, 
            Concurrency = new byte[8]
        };

        db.Courses.Add(course);
        var coursesession = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(coursesession);
        var participant = new ParticipantEntity { 
            FirstName = "Firstname", 
            LastName = "Lastname", 
            Email = "enrollmentupdate@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(participant);

        var enrollment = new EnrollmentEntity { 
            Participant = participant, 
            CourseSession = coursesession, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enrollment);
        await db.SaveChangesAsync();

        // Skapa en ny session att byta till
        var session2 = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session2);
        await db.SaveChangesAsync();

        //                          Act
        var model = repo.ToModel(enrollment) with { CourseSessionId = session2.Id };
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        var updated = await db.Enrollments.FirstAsync(x => x.Id == enrollment.Id);
        Assert.Equal(session2.Id, updated.CourseSessionId);
    }




    //                                      DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var course = new CourseEntity { 
            Title = "Deletetitle", 
            CourseCode = 104,
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        var coursesession = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(coursesession);
        var participant = new ParticipantEntity { 
            FirstName = "Firstname", 
            LastName = "Lastname", 
            Email = "enrollmentdelete@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(participant);
        var enrollment = new EnrollmentEntity {
            Participant = participant,
            CourseSession = coursesession, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enrollment);
        await db.SaveChangesAsync();

        //                          Act
        db.Enrollments.Remove(enrollment);
        await db.SaveChangesAsync();

        //                          Assert
        Assert.False(await db.Enrollments.AnyAsync(x => x.Id == enrollment.Id));
    }
}