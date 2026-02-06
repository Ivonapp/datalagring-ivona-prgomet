using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Entities;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class TeacherMapper
    {


        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity
        public static TeacherDto ToTeacherDto(TeacherEntity entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Major = entity.Major,
            CreatedAt = entity.CreatedAt

        };
    }
}