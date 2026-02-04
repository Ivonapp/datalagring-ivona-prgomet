using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class CourseSessionMapper
    {


        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity
        public static CourseSessionDto ToCourseDto(CourseSessionEntity entity) => new()
        {
            Id = entity.Id,
            CourseId = entity.CourseId,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}