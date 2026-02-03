
namespace LearningPlatform.Infrastructure.Models;

public class CourseSessionEntity
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public byte[] Concurrency { get; set; } = null!; //SÄKERHET
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }





    //RELATION I INFRASTRUKTUREDBCONTEXT
    public CourseEntity Course { get; set; } = null!;
    public ICollection<EnrollmentEntity> Enrollments { get; set; } = [];
    public ICollection<TeacherEntity> Teachers { get; set; } = [];
}
