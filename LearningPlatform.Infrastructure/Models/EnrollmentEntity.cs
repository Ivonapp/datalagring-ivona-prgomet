
namespace LearningPlatform.Infrastructure.Models;

public class EnrollmentEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public DateTime EnrollmentDate { get; set; }

    public int ParticipantId { get; set; } //TILLAGD FÖR RELATION I DBCONTEXT
    public int CourseSessionId { get; set; } //TILLAGD FÖR RELATION I DBCONTEXT







    public CourseSessionEntity CourseSession { get; set; } = null!;
    public ParticipantEntity Participant { get; set; } = null!;
}
