
namespace LearningPlatform.Infrastructure.Models;

public class CourseSessionEntity
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
