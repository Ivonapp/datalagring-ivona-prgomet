
namespace LearningPlatform.Infrastructure.Models;


//BYTA NAMN PÅ MAPPEN MODELS TILL ENTITIES ISTÄLLET
public class EnrollmentEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET. Denna ska ENDAST finnas i Infrastructure. Emil använder "RowVersion" men jag kör på Hans version "Concurrency"
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public int ParticipantId { get; set; }          //Tillagd för RELATION i InfrastructureDbContext
    public int CourseSessionId { get; set; }        //Tillagd för RELATION i InfrastructureDbContext



    //RELATION I INFRASTRUKTUREDBCONTEXT
    public CourseSessionEntity CourseSession { get; set; } = null!;
    public ParticipantEntity Participant { get; set; } = null!;
}





