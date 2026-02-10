using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.Outputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using LearningPlatform.Application.Mappers;
using LearningPlatform.Application.Services;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entities.ValueObjects;
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



    // CREATE
    public async Task<int> CreateAsync(CourseSessionInput input, CancellationToken ct = default)
    {

        var sessionToCreate = new CourseSessionModel(
            0,
            input.CourseId,
            Array.Empty<byte>(),
            input.StartDate,
            input.EndDate,
            DateTime.UtcNow,
            null
        );

        await courseSession.AddAsync(sessionToCreate, ct);
        await uow.SaveChangesAsync(ct);

        return sessionToCreate.Id;
    }

    // DELETE
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await courseSession.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"CourseSession {id} not found.");

        await courseSession.UpdateAsync(existing, ct);
        await courseSession.DeleteAsync(id, ct);
        await uow.SaveChangesAsync(ct);
    }


    // GET BY ID
    public async Task<CourseSessionOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var model = await courseSession.GetByIdAsync(id, ct);
        return model is null ? null : CourseSessionMapper.ToOutput(model);
    }


    // LIST
    public async Task<IReadOnlyList<CourseSessionOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await courseSession.ListAsync(ct);
        return CourseSessionMapper.ToOutputList(models);
    }


    // UPDATE
    public async Task UpdateAsync(int id, CourseSessionInput input, CancellationToken ct = default)
    {
        var existing = await courseSession.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"CourseSession {id} not found.");


        var model = CourseSessionMapper.ToModel(input) with
        {
            Id = id,
            Concurrency = existing.Concurrency,
            UpdatedAt = DateTime.UtcNow
        };

        await courseSession.UpdateAsync(model, ct);
        await uow.SaveChangesAsync(ct);

    }
}