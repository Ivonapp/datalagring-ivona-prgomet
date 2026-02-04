namespace LearningPlatform.Infrastructure.Models;

public class CourseEntity
{
    public int Id { get; set; }
    public int CourseCode { get; set; } //DENNA ÄR TILLAGD EFTER COURSEMAPPER
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET. Denna ska ENDAST finnas i CourseEntity
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }



    //RELATION I INFRASTRUKTUREDBCONTEXT
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
}
