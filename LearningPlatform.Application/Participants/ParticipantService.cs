using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using LearningPlatform.Application.Services;
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
    public Task CreateAsync(ParticipantInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<ParticipantOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ParticipantOutput>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, ParticipantInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}





