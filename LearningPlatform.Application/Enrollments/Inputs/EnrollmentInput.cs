using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments.Inputs;



// NY RECORD

public sealed record EnrollmentInput(
    int ParticipantId,
    int CourseSessionId

    );





// GAMMAL KLASS NEDAN
/*
 public class EnrollmentInput
{
    public int ParticipantId { get; set; }                          // ANVÄNDARENS STUDENT ID
    public int CourseSessionId { get; set; }                        // KURSEN ANVÄNDAREN VÄLJER
}
*/
