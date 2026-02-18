using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.Courses.Outputs;
using LearningPlatform.Application.Courses.PersistenceModels;
using LearningPlatform.Application.CourseSessions.Outputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;



//                 NY KOD
//                 Entity <-> Model

        namespace LearningPlatform.Application.Mappers;

        public class CourseMapper
        {

            //                  OUTPUTS
            public static CourseOutput ToOutput(CourseModel model) => new(
                model.Id,
                model.CourseCode,
                model.Title,
                model.Description,
                model.CreatedAt
            );



            //                  INPUTS - CourseInput

    // (DET ANVÄNDAREN SKRIVER IN! CreatedAt behövs alltså inte här, då ju användaren inte skriver in dagens datum te.x.)
    // Har äntligen förstått nedan del, och varför man skriver som man gör. I detta fallet har INPUTS endast ParticipantId OCH CourseSessionId,
    // Men eftersom EnrollmentModel har 6 element, så behöver vi fylla i 6 element nedan med.
    // I alla andra element än ParticipantId och CourseSessionId fyller vi alltså i 0, [], DateTime.UtcNow och null. 
            public static CourseModel ToModel(CourseInput input) => new(
                0,
                input.CourseCode,
                [],
                input.Title,
                input.Description,
                DateTime.UtcNow,
                null,
                input.TeacherId
            );




    public static IReadOnlyList<CourseOutput> ToOutputList(IEnumerable<CourseModel> models) //ALREADY ADDED SO I DON'T HAVE TO LATER.
                => models.Select(ToOutput).ToList();





}






















//                              GAMMAL KOD
//                              Entity <-> DTO


/*using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class CourseMapper
    {

        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity
        public static CourseDto ToCourseDto(CourseEntity entity) => new()
        {

            Id = entity.Id,
            CourseCode = entity.CourseCode,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt

        };
    }
}*/

