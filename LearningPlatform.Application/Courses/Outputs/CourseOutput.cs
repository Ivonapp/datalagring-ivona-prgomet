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

    // GAMMAL KLASS
    /*public class CourseOutput
    {
        public int Id { get; set; }
        public int CourseCode { get; set; }
        public byte[] Concurrency { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    } */

   