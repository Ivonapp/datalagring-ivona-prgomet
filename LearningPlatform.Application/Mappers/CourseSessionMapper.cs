using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.Outputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;


//                 NY KOD
//                 Entity <-> Model

namespace LearningPlatform.Application.Mappers;

public class CourseSessionMapper
{

    //                  OUTPUTS
    public static CourseSessionOutput ToOutput(CourseSessionModel model) => new()
    {
        Id = model.Id,
        CourseId = model.CourseId,
        StartDate = model.StartDate,
        EndDate = model.EndDate,
        CreatedAt = model.CreatedAt,
        UpdatedAt = model.UpdatedAt
    };


    //                  INPUTS - CourseSessionInput

    // (DET ANVÄNDAREN SKRIVER IN! CreatedAt behövs alltså inte här, då ju användaren inte skriver in dagens datum te.x.)
    // Har äntligen förstått nedan del, och varför man skriver som man gör. I detta fallet har INPUTS endast ParticipantId OCH CourseSessionId,
    // Men eftersom EnrollmentModel har 6 element, så behöver vi fylla i 6 element nedan med.
    // I alla andra element än ParticipantId och CourseSessionId fyller vi alltså i 0, [], DateTime.UtcNow och null. 
    public static CourseSessionModel ToModel(CourseSessionInput input) => new(

        0,                        // 1: Id
        input.CourseId,           // 2: CourseId. Vilken kurs tillhör detta tillfälle? (T.ex. Kurs-ID 5).
        [],                       // 3: Concurrency
        input.StartDate,          // 4: StartDate. När börjar kursen? (T.ex. 2026-01-01).
        input.EndDate,            // 5: EndDate. När slutar kursen? (T.ex. 2026-06-01).
        DateTime.UtcNow,          // 6: CreatedAt
        null
    );
}
























//                              GAMMAL KOD
//                              Entity <-> DTO


/*using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class CourseSessionMapper
    {


        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity

        public static CourseSessionDto ToCourseSessionDto(CourseSessionEntity entity) => new()
        {
            Id = entity.Id,
            CourseId = entity.CourseId,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}*/