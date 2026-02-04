using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***



//Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
//                          DTOs -> Entity

namespace LearningPlatform.Application.Mappers
{
    public class CourseMapper
    {


        //"MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //(CourseDto.cs -> CourseEntity.cs)
        public static CourseDto ToCourseDto(CourseEntity entity) => new()
        {
            CourseCode = entity.CourseCode,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt

        };
    }
}

