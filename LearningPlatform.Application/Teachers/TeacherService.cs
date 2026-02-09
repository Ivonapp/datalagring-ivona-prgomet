using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Services;
using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Teachers;

public sealed class TeacherService
    (
    ITeacherRepository teacher,
    IUnitOfWork uow
    ) : ITeacherService
{
    public Task CreateAsync(TeacherInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<TeacherOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<TeacherOutput>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, TeacherInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
