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
    IUnitOfWork uow,
     ITeacherRepository teacherRepository //
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
            input.TeacherId,
            null,
            null
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
        if (model is null) return null;

        // 🔹 NY FRÅN HÄR: hämta läraren och fyll ut namnet
        var teacher = await teacherRepository.GetByIdAsync(model.TeacherId, ct);
        return new CourseOutput(
            model.Id,
            model.CourseCode,
            model.Title,
            model.Description,
            model.CreatedAt,
            model.TeacherId,
            teacher?.FirstName ?? "",   //  TeacherFirstName
            teacher?.LastName ?? ""     //  TeacherLastName
        );
    }



    // LIST /////////////////
    public async Task<IReadOnlyList<CourseOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await course.ListAsync(ct);
        var list = new List<CourseOutput>();

        // 🔹 Fyll ut lärarnamn för alla kurser
        foreach (var model in models)
        {
            var teacher = await teacherRepository.GetByIdAsync(model.TeacherId, ct);

            list.Add(new CourseOutput(
                model.Id,
                model.CourseCode,
                model.Title,
                model.Description,
                model.CreatedAt,
                model.TeacherId,
                teacher?.FirstName ?? "",
                teacher?.LastName ?? ""
            ));
        }

        return list;
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