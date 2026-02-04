
namespace LearningPlatform.Application.DTOs;

public class CourseDto
{
    public int Id { get; set; }

    public int CourseCode { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
