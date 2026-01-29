namespace LearningPlatform.Application.DTOs;

public class CourseSessionDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int CourseId { get; set; }
}
