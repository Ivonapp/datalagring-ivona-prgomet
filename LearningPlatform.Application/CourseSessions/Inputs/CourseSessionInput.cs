using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.CourseSessions.Inputs;


// NY RECORD 
public sealed record CourseSessionInput(
    int CourseId,
    DateTime StartDate,
    DateTime? EndDate
    );




//GAMMAL KLASS NEDAN
/*public class CourseSessionInput
{

public int CourseId { get; set; }
public DateTime StartDate { get; set; }
public DateTime? EndDate { get; set; }
}
*/

