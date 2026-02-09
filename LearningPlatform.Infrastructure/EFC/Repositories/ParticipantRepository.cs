
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


//PÅGÅENDE 

namespace LearningPlatform.Infrastructure.EFC.Repositories;

public class ParticipantRepository(InfrastructureDbContext Context) : EfcRepositoryBase<ParticipantEntity, int, ParticipantModel>(Context), IParticipantRepository
{
   
    //TOMODEL
    public override ParticipantModel ToModel(ParticipantEntity entity) => new(
            entity.Id,
            entity.Concurrency,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.PhoneNumber,
            entity.CreatedAt,
            entity.UpdatedAt
    );



    //ADDASYNC

    public override async Task AddAsync(ParticipantModel model, CancellationToken ct = default)
    {
        //if (model.Id == 0) //Chatgpt hjälpte mig med att det endast kan vara 0 för int, och inte "empty." som man gör med Guid.
        //throw new ArgumentException("Participant Id must be set by the application layer");  <-- DENNA RADEN ÄR TILLFÄLLIGT UTKOMMENTERAD DÅ DET INTE VERKAT SOM == 0 FUNKAR HÄR. SAMMA SAK I TEACHERREPOSITORY.


        var entity = new ParticipantEntity
        {
            //Id = model.Id, SE OVAN UTKOMMENTAR
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        await Set.AddAsync(entity, ct);               // Lägger till i kön - (utan denna och koden nedan händer ingenting.)
    }


    //UPDATE
    public override async Task UpdateAsync(ParticipantModel model, CancellationToken ct = default)
    {
        var entity = await Set.SingleOrDefaultAsync(x => x.Id == model.Id, ct)
            ?? throw new ArgumentException($"Participant {model.Id} not found.");

        Context.Entry(entity).Property(x => x.Concurrency).OriginalValue = model.Concurrency;

        // 1. DATA SOM SKA GÅ ATT ÄNDRA!
        entity.FirstName = model.FirstName;
        entity.LastName = model.LastName;
        entity.Email = model.Email.Trim();
        entity.PhoneNumber = model.PhoneNumber;

        // 2. TIDSTÄMPEL FÖR ÄNDRINGEN!
        entity.UpdatedAt = DateTime.UtcNow;
    }



    //EMAIL
            public async Task<bool> EmailAlreadyExistsAsync(string email, CancellationToken ct = default)

    {
        var normalized = email.Trim();

        return await Set
        .AsNoTracking()
        .AnyAsync(x => x.Email == normalized, ct);
    }
}

