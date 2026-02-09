using LearningPlatform.Application.Abstractions.Persistence;

namespace LearningPlatform.Infrastructure.EFC.Entities;


//STRUKTUR - ENTITIES LIGGER I INFRASTRUCTURE SOM HANS LAGT
public class CourseSessionEntity : IEntity<int>
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public byte[] Concurrency { get; set; } = null!;            //SÄKERHET. Denna ska ENDAST finnas i Infrastructure. Emil använder "RowVersion" men jag kör på Hans version "Concurrency"
    public DateTime StartDate { get; set; }                     // När kursen börjar för studenten
    public DateTime? EndDate { get; set; }                       // När kursen slutar för studenten
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Tidpunkt då sessionen skapades i databasen
    public DateTime? UpdatedAt { get; set; }                    // Tidpunkt för sessionens senaste ändring


    //RELATION I INFRASTRUKTUREDBCONTEXT
    public CourseEntity Course { get; set; } = null!;
    public ICollection<EnrollmentEntity> Enrollments { get; set; } = [];
    public ICollection<TeacherEntity> Teachers { get; set; } = [];
}
