using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments.Outputs;




//              OUTPUT = DET SOM SKICKAS UT TILL ANVÄNDAREN/SYSTEMET
//              JAG KOPIERAR FÖRST ALLA PROPERTIES FRÅN ENROLLMENT ENTITIES, OCH FÖRDELAR SEN PROPERTIES MELLAN INPUT OCH OUTPUT

public class EnrollmentOutput
{
    public int Id { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ParticipantId { get; set; }
    public int CourseSessionId { get; set; }
}