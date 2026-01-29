
namespace LearningPlatform.Infrastructure.Models;

public class CourseSessionEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CourseId { get; set; }
}
