using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.Outputs;
using LearningPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.CourseSessions;

public sealed class CourseSessionService
       (
    ICourseSessionRepository courseSession,
    IUnitOfWork uow
    ) : ICourseSessionService
{
    public Task CreateAsync(CourseSessionInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<CourseSessionOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<CourseSessionOutput>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, CourseSessionInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
