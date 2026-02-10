using LearningPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LearningPlatform.Application.Courses.Outputs
{


    // NY RECORD
    public sealed record CourseOutput(
        int Id,
        int CourseCode,
        string Title,
        string Description,
        DateTime CreatedAt

        );
}
   