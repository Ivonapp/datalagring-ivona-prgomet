
//FÖRSÖKER FÖRSTÅ OCH FIXA TILL DENNA DELEN > TEACHERMAPPER


namespace LearningPlatform.Application.Teachers.Inputs;

public class TeacherInput
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
}