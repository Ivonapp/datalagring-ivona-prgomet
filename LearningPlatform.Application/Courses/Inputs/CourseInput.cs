using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Courses.Inputs
{
    public class CourseInput
    {
        public int CourseCode { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
