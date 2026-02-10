using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.Outputs;
using LearningPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments;

public sealed class EnrollmentService
    (
    IEnrollmentRepository enrollment,
    IUnitOfWork uow
    ) : IEnrollmentService

{
    public Task CreateAsync(EnrollmentInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<EnrollmentOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<EnrollmentOutput>> ListAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, EnrollmentInput input, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}