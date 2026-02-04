namespace LearningPlatform.Infrastructure.Models;

public class CourseEntity
{
    public int Id { get; set; }
    public int CourseCode { get; set; }                 //DENNA ÄR TILLAGD EFTER COURSEMAPPER
    public byte[] Concurrency { get; set; } = null!;    //SÄKERHET. Denna ska ENDAST finnas i Infrastructure. Emil använder "RowVersion" men jag kör på Hans version "Concurrency"
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }



    //RELATION I INFRASTRUKTUREDBCONTEXT
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
}
