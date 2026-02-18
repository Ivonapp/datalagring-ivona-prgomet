using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.Courses.Outputs;
using LearningPlatform.Application.Courses.PersistenceModels;
using LearningPlatform.Application.Mappers;
using LearningPlatform.Application.Services;
using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses;

public sealed class CourseService
    (
    ICourseRepository course,
    IUnitOfWork uow
    ) : ICourseService

{

    // CREATE
    public async Task<int> CreateAsync(CourseInput input, CancellationToken ct = default)
    {

        var courseToCreate = new CourseModel(
            0,
            input.CourseCode,
            null,
            input.Title,
            input.Description,
            DateTime.UtcNow,
            null,
            input.TeacherId
        );

        await course.AddAsync(courseToCreate, ct);
        await uow.SaveChangesAsync(ct);

        return 1;
    }


    // DELETE
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await course.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"Course {id} not found.");

        await course.UpdateAsync(existing, ct);
        await course.DeleteAsync(id, ct);
        await uow.SaveChangesAsync(ct);
    }

    // GET BY ID
    public async Task<CourseOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var model = await course.GetByIdAsync(id, ct);
        return model is null ? null : CourseMapper.ToOutput(model);
    }



    // LIST
    public async Task<IReadOnlyList<CourseOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await course.ListAsync(ct);
        return CourseMapper.ToOutputList(models);
    }


    // UPDATE
    public async Task UpdateAsync(int id, CourseInput input, CancellationToken ct = default)
    {
        var existing = await course.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"Course {id} not found.");

        var model = CourseMapper.ToModel(input) with
        {
            Id = id,
            Concurrency = existing.Concurrency,
            UpdatedAt = DateTime.UtcNow
        };

        await course.UpdateAsync(model, ct);
        await uow.SaveChangesAsync(ct);

    }
}