using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using LearningPlatform.Application.Participants.PersistenceModels;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***


        //                 NY KOD
        //                 Entity <-> Model

    namespace LearningPlatform.Application.Mappers;
    public class ParticipantMapper
    {

        //                  OUTPUTS
        public static ParticipantOutput ToOutput(ParticipantModel model) => new()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = model.CreatedAt
        };


        //                  NEDAN FICK HJÄLP AV CHATGPT
        //                  ParticipantInput

        //(DET ANVÄNDAREN SKRIVER IN! CreatedAt behövs alltså inte då ju användaren inte skriver in dagens datum te.x.)
        public static ParticipantModel ToModel(ParticipantInput input) => new(
            0,
            input.FirstName,
            input.LastName,
            input.Email,
            input.PhoneNumber,
            DateTime.UtcNow
        );
    }

























//                              GAMMAL KOD
//                              Entity <-> DTO


/*using LearningPlatform.Application.DTOs;
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
}*/
