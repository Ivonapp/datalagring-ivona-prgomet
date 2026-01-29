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
    

    //hHär nedan definierar vi HUR strukturen ska se ut i självaste DATABASEN: (Hans video 24:46 Använd kodbaserad modell för SQL-databas med Entity Framework Core (Code first)
    //Koden ovanför med alla public DbSet ska sen tas bort när nedan är färdig, ovan är bara en mall atm.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}



