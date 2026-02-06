namespace LearningPlatform.Infrastructure.Entities;


//STRUKTUR - ENTITIES LIGGER I INFRASTRUCTURE SOM HANS LAGT
public class ParticipantEntity
{
    public int Id { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  //När användaren skapades
    public DateTime? UpdatedAt { get; set; }                    //När användaren uppdaterades senast


    //RELATION I INFRASTRUKTUREDBCONTEXT
    public ICollection<EnrollmentEntity> Enrollments { get; set; } = [];
}
