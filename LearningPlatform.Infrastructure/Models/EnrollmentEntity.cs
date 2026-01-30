
namespace LearningPlatform.Infrastructure.Models;

public class EnrollmentEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public DateTime EnrollmentDate { get; set; }
}
