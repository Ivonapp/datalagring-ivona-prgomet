using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using LearningPlatform.Application.Teachers.PersistenceModels;

// *** SKapa alla klasserna som MAPPERS-klasser innan du går vidare ***






//                              NY KOD
//                              Entity <-> Model


            namespace LearningPlatform.Application.Mappers;

            public class TeacherMapper
            {



            //                  OUTPUTS
                public static TeacherOutput ToOutput(TeacherModel model) => new()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Major = model.Major,
                    CreatedAt = model.CreatedAt
                };


    //                  NEDAN FICK HJÄLP AV CHATGPT
    //                  INPUTS
                public static TeacherModel ToModel(TeacherInput input) => new(
                    0,
                    input.FirstName,
                    input.LastName,
                    input.Email,
                    input.PhoneNumber,
                    input.Major,
                    [],
                    DateTime.UtcNow,
                    null
                );
        }





















//                              GAMMAL KOD
//                              Entity <-> DTO



/*

using LearningPlatform.Application.DTOs;
using LearningPlatform.Infrastructure.Entities;


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
}*/