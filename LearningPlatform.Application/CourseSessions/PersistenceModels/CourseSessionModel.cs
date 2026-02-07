using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.CourseSessions.PersistenceModels;

public sealed record CourseSessionModel(
    int Id,
    int CourseId,
    byte[] Concurrency,
    DateTime StartDate,
    DateTime? EndDate,
    DateTime CreatedAt,
    DateTime? UpdatedAt
        );
