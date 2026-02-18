using LearningPlatform.Application.Abstractions.Persistence;

namespace LearningPlatform.Infrastructure.EFC.Entities;


//STRUKTUR - ENTITIES LIGGER I INFRASTRUCTURE SOM HANS LAGT
public class TeacherEntity : IEntity<int>
{

    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Major { get; set; } = null!;
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;      //När användaren skapades
    public DateTime? UpdatedAt { get; set; }                        //När användaren uppdaterades senast




    //RELATION I INFRASTRUKTUREDBCONTEXT

    public ICollection<CourseEntity> Courses { get; set; } = []; //Tillagd nyss

    public ICollection<CourseSessionEntity> CourseSessions { get; set; } = [];
}
