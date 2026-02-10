using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Mappers;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using LearningPlatform.Application.Participants.PersistenceModels;
using LearningPlatform.Application.Services;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Participants;

public sealed class ParticipantService
    (
    IParticipantRepository participant,
    IUnitOfWork uow
    ) : IParticipantService
{
    // CREATE
    public async Task<int> CreateAsync(ParticipantInput input, CancellationToken ct = default)
    {

        var email = new Email(input.Email);
        var phoneNumber = string.IsNullOrWhiteSpace(input.PhoneNumber)
            ? null
            : new PhoneNumber(input.PhoneNumber);

        if (await participant.EmailAlreadyExistsAsync(email.Value, ct))
        {
            throw new ArgumentException("Participant with this email already exists.");
        }


        var participantToCreate = new ParticipantModel(
                0,
                Array.Empty<byte>(),
                input.FirstName,
                input.LastName,
                email.Value,
                input.PhoneNumber,
                DateTime.UtcNow,
                null
            );


        await participant.AddAsync(participantToCreate, ct);
        await uow.SaveChangesAsync(ct);


        return participantToCreate.Id;

    }



    // DELETE
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await participant.GetByIdAsync(id, ct)
            ?? throw new ArgumentException("Participant not found");

        await participant.UpdateAsync(existing, ct);
        await participant.DeleteAsync(id, ct);
        await uow.SaveChangesAsync(ct);
    }


    // GET BY ID
    public async Task<ParticipantOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var model = await participant.GetByIdAsync(id, ct);
        return model is null ? null : ParticipantMapper.ToOutput(model);
    }



    // IREADONLYLIST
    public async Task<IReadOnlyList<ParticipantOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await participant.ListAsync(ct);
        return ParticipantMapper.ToOutputList(models);
    }



    // UPDATE
    public async Task UpdateAsync(int id, ParticipantInput input, CancellationToken ct = default)
    {
        var existing = await participant.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"Participant {id} not found.");


        var email = new Email(input.Email);
        var model = ParticipantMapper.ToModel(input) with
        {
            Id = id,
            Concurrency = existing.Concurrency,
            Email = email.Value
        };

        await participant.UpdateAsync(model, ct);
        await uow.SaveChangesAsync(ct);

    }
}










