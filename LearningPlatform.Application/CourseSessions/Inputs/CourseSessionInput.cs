using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.CourseSessions.Inputs;

public class CourseSessionInput
{

public int CourseId { get; set; }
public DateTime StartDate { get; set; }
public DateTime? EndDate { get; set; }
}