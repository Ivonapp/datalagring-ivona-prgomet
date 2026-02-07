using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses.PersistenceModels;

public sealed record CourseModel(
        int Id,
        int CourseCode,
        byte[] Concurrency,
        string Title,
        string Description,
        DateTime CreatedAt,
        DateTime? UpdatedAt
);


