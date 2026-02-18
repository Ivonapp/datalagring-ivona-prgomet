using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses.Inputs
{
    // NY RECORD

    public sealed record CourseInput(
        int CourseCode,
        string Title,
        string Description,
        int TeacherId
        );
}
