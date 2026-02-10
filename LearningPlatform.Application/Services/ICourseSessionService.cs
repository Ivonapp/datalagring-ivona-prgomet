using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Services;

public interface ICourseSessionService
{
    // C - Create
    Task CreateAsync(CourseSessionInput input, CancellationToken ct = default);

    // R - Read (Hämta en)
    Task<CourseSessionOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    // R - Read (Hämta alla)
    Task<IReadOnlyList<CourseSessionOutput>> ListAsync(CancellationToken ct = default);

    // U - Update
    Task UpdateAsync(int id, CourseSessionInput input, CancellationToken ct = default);

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