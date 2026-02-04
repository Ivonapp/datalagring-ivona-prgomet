namespace LearningPlatform.Domain.Entities;
public class Enrollment
{
    public int Id { get; set; }
    public int CourseSessionId { get; set; }        //kopplingen till CourseSession. Lite osäker på denna men låter den vara sålänge. 
    public int ParticipantId { get; set; }          //kopplingen till participant. Lite osäker på denna men låter den vara sålänge. 
    public DateTime EnrollmentDate { get; set; }
}
