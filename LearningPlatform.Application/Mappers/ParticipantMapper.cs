using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Models;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


namespace LearningPlatform.Application.Mappers
{
    public class ParticipantMapper
    {


        //Mappers är "MEDLARE" MELLAN APPLICATION DTO OCH INFRASTRUCTURE MODELS
        //                          DTOs -> Entity
        public static ParticipantDto ToParticipantDto(ParticipantEntity entity) => new()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            CreatedAt = entity.CreatedAt

        };
    }
}
