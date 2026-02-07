using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.CourseSessions.Outputs;

public class CourseSessionOutput
{

    public int Id { get; set; }
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}


