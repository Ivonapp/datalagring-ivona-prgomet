using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using System;
using System.Collections.Generic;
using System.Text;


//PÅGÅENDE 

namespace LearningPlatform.Infrastructure.EFC.Repositories;

public class ParticipantRepository(InfrastructureDbContext Context) : EfcRepositoryBase<ParticipantEntity, int, ParticipantModel>(Context), IParticipantRepository
{
    public override Task AddAsync(ParticipantModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public override ParticipantModel ToModel(ParticipantEntity entity)
    {
        throw new NotImplementedException();
    }

    public override Task UpdateAsync(ParticipantModel model, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }





    public Task<bool> EmailAlreadyExistsAsync(string email, CancellationToken ct = default)  //denna koden kommer enskilt från IParticipantRepository, tillskillnad från dom andra där jag inte än skrivit något.
    {
        throw new NotImplementedException();
    }
}



