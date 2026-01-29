using LearningPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Data;

//PÅGÅENDE KOD. Denna är inte färdig. Försöker få ihop nån sorts struktur som jag förstår.
public sealed class InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : DbContext(options)
{
   
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<CourseSessionEntity> CourseSessions { get; set; }
    public DbSet<EnrollmentEntity> Enrollments { get; set; }
    public DbSet<ParticipantEntity> Participants { get; set; }
    public DbSet<TeacherEntity> Teachers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}



