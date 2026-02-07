using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments.PersistenceModels;
public sealed record EnrollmentModel(
        int Id,
        byte[] Concurrency,
        DateTime EnrollmentDate,
        DateTime? UpdatedAt,
        int ParticipantId,
        int CourseSessionId
            );

