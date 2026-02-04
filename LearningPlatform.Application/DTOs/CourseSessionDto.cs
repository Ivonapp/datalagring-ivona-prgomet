namespace LearningPlatform.Application.DTOs;

public class CourseSessionDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public DateTime StartDate { get; set; }     // När kursen börjar för studenten
    public DateTime EndDate { get; set; }       // När kursen slutar för studenten
    public DateTime CreatedAt { get; set; }     // Tidpunkt då sessionen skapades i databasen
    public DateTime? UpdatedAt { get; set; }    // Tidpunkt för sessionens senaste ändring

}