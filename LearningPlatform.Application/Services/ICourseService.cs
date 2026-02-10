using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.Courses.Outputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Services;

public interface ICourseService
{
    // C - Create
    Task<int> CreateAsync(CourseInput input, CancellationToken ct = default);

    // R - Read (Hämta en)
    Task<CourseOutput?> GetByIdAsync(int id, CancellationToken ct = default);

    // R - Read (Hämta alla)
    Task<IReadOnlyList<CourseOutput>> ListAsync(CancellationToken ct = default);

    // U - Update
    Task UpdateAsync(int id, CourseInput input, CancellationToken ct = default);

    // D - Delete
    Task DeleteAsync(int id, CancellationToken ct = default);
}

