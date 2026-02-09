
namespace LearningPlatform.Application.Teachers.Inputs;

//KOD FÖR ITEACHERSERVICE 
// Gjort om den tidigare klassen till en record (?)

public sealed record TeacherInput(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Major
);




//GAMLA - INTE SÄKER PÅ OM OVAN STÄMMER

/*public class TeacherInput
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
}*/