using LearningPlatform.Application.DTOs;
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
}