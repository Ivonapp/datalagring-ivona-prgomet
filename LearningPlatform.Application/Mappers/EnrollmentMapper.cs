using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.Outputs;
using LearningPlatform.Application.Enrollments.PersistenceModels;


//                 NY KOD
//                 Entity <-> Model

            namespace LearningPlatform.Application.Mappers;

            public class EnrollmentMapper
            {

                //                  OUTPUTS
                public static EnrollmentOutput ToOutput(EnrollmentModel model) => new(
                    model.Id,
                    model.EnrollmentDate,
                    model.UpdatedAt,
                    model.ParticipantId,
                    model.CourseSessionId
                );


                //                  INPUTS - EnrollmentInput

                // (DET ANVÄNDAREN SKRIVER IN! CreatedAt behövs alltså inte här, då ju användaren inte skriver in dagens datum te.x.)
                // Har äntligen förstått nedan del, och varför man skriver som man gör. I detta fallet har INPUTS endast ParticipantId OCH CourseSessionId,
                // Men eftersom EnrollmentModel har 6 element, så behöver vi fylla i 6 element nedan med.
                // I alla andra element än ParticipantId och CourseSessionId fyller vi alltså i 0, [], DateTime.UtcNow och null. 
                public static EnrollmentModel ToModel(EnrollmentInput input) => new(
                        0,                      // 1: ID. (Användaren vet inte detta) -> Vi sätter 0.
                        [],                     // 2: Concurrency: (Användaren vet inte vad detta är) -> Vi sätter [] (tom array).
                        DateTime.UtcNow,        // 3: EnrollmentDate: (Användaren ska inte skriva datumet själv) -> Vi sätter DateTime.UtcNow.
                        null,                   // 4: UpdatedAt: (Finns ingen uppdatering än) -> Vi sätter null.
                        input.ParticipantId,    // 5: Hämtas från Input.
                        input.CourseSessionId   // 6: Hämtas från Input.
                );
            }





















//                              GAMMAL KOD
//                              Entity <-> DTO


/*using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class EnrollmentMapper
    {


        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity
        public static EnrollmentDto ToEnrollmentDto(EnrollmentEntity entity) => new()
        {
            Id = entity.Id,
            CourseSessionId = entity.CourseSessionId,
            ParticipantId = entity.ParticipantId,
            EnrollmentDate = entity.EnrollmentDate

        };
    }
}*/