using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Services;

public interface IEnrollmentService
{
    // C - Create
    Task CreateAsync(EnrollmentInput input, CancellationToken ct = default);

    // R - Read (Hämta en)
    Task<EnrollmentOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    // R - Read (Hämta alla)
    Task<IReadOnlyList<EnrollmentOutput>> ListAsync(CancellationToken ct = default);

    // U - Update
    Task UpdateAsync(int id, EnrollmentInput input, CancellationToken ct = default);

    // D - Delete
    Task DeleteAsync(int id, CancellationToken ct = default);
}




/*
    Task CreateAsync(ParticipantInput input, CancellationToken ct = default);

    Task<ParticipantOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    Task<IReadOnlyList<ParticipantOutput>> ListAsync(CancellationToken ct = default);

    Task UpdateAsync(int id, ParticipantInput input, CancellationToken ct = default);

    Task DeleteAsync(int id, CancellationToken ct = default);
*/