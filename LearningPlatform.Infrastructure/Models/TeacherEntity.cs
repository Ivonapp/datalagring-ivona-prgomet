
namespace LearningPlatform.Infrastructure.Models;

public class TeacherEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
}
