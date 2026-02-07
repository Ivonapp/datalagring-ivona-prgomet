

//FÖRSÖKER FÖRSTÅ OCH FIXA TILL DENNA DELEN > TEACHERMAPPER


namespace LearningPlatform.Application.Teachers.Outputs;

public class TeacherOutput
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}