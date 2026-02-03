namespace LearningPlatform.Infrastructure.Models;

public class CourseEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public string Title { get; set; }
    public string Description { get; set; }



    //RELATION I INFRASTRUKTUREDBCONTEXT
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
}
