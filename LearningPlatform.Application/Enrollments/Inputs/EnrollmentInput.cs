using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments.Inputs;




//              INPUT = DET ANVÄNDAREN FAKTISKT FYLLER I
//              JAG KOPIERAR FÖRST ALLA PROPERTIES FRÅN ENROLLMENT ENTITIES, OCH FÖRDELAR SEN PROPERTIES MELLAN INPUT OCH OUTPUT

public class EnrollmentInput
{
    public int ParticipantId { get; set; }                          // ANVÄNDARENS STUDENT ID
    public int CourseSessionId { get; set; }                        // KURSEN ANVÄNDAREN VÄLJER
}

