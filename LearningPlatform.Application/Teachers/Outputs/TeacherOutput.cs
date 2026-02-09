
namespace LearningPlatform.Application.Teachers.Outputs;




//KOD FÖR ITEACHERSERVICE 
// Gjort om den tidigare klassen till en record (?)

public sealed record TeacherOutput(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Major,
    DateTime CreatedAt
);





//GAMLA - INTE SÄKER PÅ OM OVAN STÄMMER

/*public class TeacherOutput
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}*/