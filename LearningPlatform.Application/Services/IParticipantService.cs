using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Services;

public interface IParticipantService
{

    // C - Create
    // Skapar en ny deltagare utifrån ParticipantInput
    Task<int> CreateAsync(ParticipantInput input, CancellationToken ct = default);

    // R - Read (Hämta en)
    // Returnerar en ParticipantOutput om den finns
    Task<ParticipantOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    // R - Read (Hämta alla)
    // Returnerar en lista med ALLA deltagare (ParticipantOutput)
    Task<IReadOnlyList<ParticipantOutput>> ListAsync(CancellationToken ct = default);

    // U - Update
    // Uppdaterar en befintlig deltagare med id och ny data från ParticipantInput
    Task UpdateAsync(int id, ParticipantInput input, CancellationToken ct = default);

    // D - Delete
    // Tar bort en deltagare baserat på id
    Task DeleteAsync(int id, CancellationToken ct = default);
}
