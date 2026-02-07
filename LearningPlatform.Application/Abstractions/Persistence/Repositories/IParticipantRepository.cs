
using LearningPlatform.Application.Participants.PersistenceModels;

namespace LearningPlatform.Application.Abstractions.Persistence.Repositories;

public interface IParticipantRepository : IRepositoryBase<ParticipantModel, int>
{
    Task<bool> EmailAlreadyExistsAsync(string email, CancellationToken ct = default);
}