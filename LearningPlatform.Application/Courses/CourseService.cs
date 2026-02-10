using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.Courses.Outputs;
using LearningPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses;

public sealed class CourseService
    (
    ICourseRepository course,
    IUnitOfWork uowÖ
    ) : ICourseService

{
    public Task CreateAsync(CourseInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<CourseOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<CourseOutput>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, CourseInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
