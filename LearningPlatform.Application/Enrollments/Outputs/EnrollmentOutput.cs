using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments.Outputs;



// NY RECORD OVAN

public sealed record EnrollmentOutput(
    int Id,
    DateTime EnrollmentDate,
    DateTime? UpdatedAt,
    int ParticipantId,
    int CourseSessionId

    );






// GAMMAL KLASS NEDAN
/*public class EnrollmentOutput
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ParticipantId { get; set; }
    public int CourseSessionId { get; set; }
}*/