
namespace LearningPlatform.Infrastructure.Models;


//BYTA NAMN PÅ MAPPEN MODELS TILL ENTITIES ISTÄLLET
public class TeacherEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;      //När användaren skapades
    public DateTime? UpdatedAt { get; set; }                        //När användaren uppdaterades senast




    //RELATION I INFRASTRUKTUREDBCONTEXT
    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];

}
