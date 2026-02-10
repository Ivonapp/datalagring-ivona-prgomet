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
        public static ParticipantOutput ToOutput(ParticipantModel model) => new(
            model.Id,
            model.FirstName,
            model.LastName,
            model.Email,
            model.PhoneNumber,
            model.CreatedAt
        );


        //        INPUTS
        //        FICK HJÄLP AV CHATGPT NEDAN - fick hjälp av chatgpt med att förstå hur denna delen skulle "se ut" samt förklaring på var och en av raderna.
        //                  
        //        Update: Har nu fixat och ändrat rätt mycket i min tidigare kod. Koden nedan skrevs först när jag höll på med mina repos,
        //        Och när jag sen började bygga på Service så behövdes det göra ändringar av denna koden.
        //        Jag har rättat till denna delen nedan (utan chatgpt) och förstått upplägget. (Följ klasserna för entity för RÄTT ORDNING på dom olika raderna.)
        //        Jag hade råkat missa att skriva en [] för concurrency och null på slutet, vilket gav mig röda squigglys. Detta är nu fixat.
        public static ParticipantModel ToModel(ParticipantInput input) => new(
            0,
            [],
            input.FirstName,
            input.LastName,
            input.Email,
            input.PhoneNumber,
            DateTime.UtcNow,
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
