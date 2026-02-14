using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.Infrastructure.EFC.Data;

public sealed class InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : DbContext(options)

{
    public DbSet<CourseEntity> Courses { get; set; } //Såg att Emil hade såhär i sin kod. La till S efter alla klassnamn. Hans hade lite annorlunda i sin video. Kanske förändrar senare.
    public DbSet<CourseSessionEntity> CourseSessions { get; set; } //Såg att Emil hade såhär i sin kod. La till S efter alla klassnamn. Hans hade lite annorlunda i sin video. Kanske förändrar senare.
    public DbSet<EnrollmentEntity> Enrollments { get; set; } //Såg att Emil hade såhär i sin kod. La till S efter alla klassnamn. Hans hade lite annorlunda i sin video. Kanske förändrar senare.
    public DbSet<ParticipantEntity> Participants { get; set; } //Såg att Emil hade såhär i sin kod. La till S efter alla klassnamn. Hans hade lite annorlunda i sin video. Kanske förändrar senare.
    public DbSet<TeacherEntity> Teachers { get; set; } //Såg att Emil hade såhär i sin kod. La till S efter alla klassnamn. Hans hade lite annorlunda i sin video. Kanske förändrar senare.


    //(Allt nedan har klurats ihop med Hans videos. INGENTING FRÅN CHATGPT.)

    //OnModelCreating = bestämmer hur tabellerna ska se ut i databasen

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



                    // COURSE 
                    modelBuilder.Entity<CourseEntity>(entity =>                         // COURSE
                {
                    entity.ToTable("Course");                                           // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Course_Id");                   // PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired(false);                                                      // SÄKERHET

                    entity.Property(e => e.Title)                                       // TITLE
                    .HasMaxLength(100)
                    .IsRequired();

                     entity.Property(e => e.Description)                                // DESCRIPTION
                     .HasMaxLength(500)
                     .IsRequired(false);

                    //TILLAGT. La till extra properties i klassen Course.
                    entity.Property(e => e.CourseCode)
                        .IsRequired();

                    entity.Property(e => e.CreatedAt)
                        .IsRequired();

                    entity.Property(e => e.UpdatedAt)
                        .IsRequired(false);




                    //Relationer - kan vara att jag ändrar sen
                    entity.HasMany(c => c.CourseSessions)
                    .WithOne(cs => cs.Course)
                    .HasForeignKey(cs => cs.CourseId);

                });





                    // COURSE SESSION
                    modelBuilder.Entity<CourseSessionEntity>(entity =>                 // COURSESESSION
                {
                    entity.ToTable("CourseSession");                                   // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_CourseSession_Id");           // PRIMARY KEY

                    entity.Property(e => e.CourseId)
                   .IsRequired();

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired(false);                                                      // SÄKERHET

                    entity.Property(e => e.StartDate)                                   // START DATUM
                    .IsRequired();

                    entity.Property(e => e.EndDate)                                     // SLUT DATUM
                    .IsRequired();


                    //TILLAGT. La till extra properties i klassen CourseSession.
                    entity.Property(e => e.CreatedAt)
                    .IsRequired();

                    entity.Property(e => e.UpdatedAt)
                    .IsRequired(false);



                    //Relationer - kan vara att jag ändrar sen
                    entity.HasOne(cs => cs.Course)
                          .WithMany(c => c.CourseSessions)
                          .HasForeignKey(cs => cs.CourseId);

                    entity.HasMany(cs => cs.Enrollments)
                          .WithOne(e => e.CourseSession)
                          .HasForeignKey(e => e.CourseSessionId);

                    entity.HasMany(cs => cs.Teachers)
                          .WithMany(t => t.CourseSessions);

                });




                    // ENROLLMENT
                    modelBuilder.Entity<EnrollmentEntity>(entity =>                     // ENROLLMENT
                {
                    entity.ToTable("Enrollment");                                       // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Enrollment_Id");               // PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired(false);                                                      // SÄKERHET

                    entity.Property(e => e.EnrollmentDate)                              // ENROLLMENTDATE
                    .IsRequired();


                    //TILLAGT. La till extra properties i klassen Enrollment.
                    entity.Property(e => e.ParticipantId)
                    .IsRequired();

                    entity.Property(e => e.CourseSessionId)
                    .IsRequired();

                    entity.Property(e => e.UpdatedAt)
                    .IsRequired(false);


                    //Relationer - kan vara att jag ändrar sen
                    entity.HasOne(e => e.CourseSession)
                    .WithMany(cs => cs.Enrollments)
                    .HasForeignKey(e => e.CourseSessionId);

                    entity.HasOne(e => e.Participant)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(e => e.ParticipantId);


                });



                    //PARTICIPANT
                    modelBuilder.Entity<ParticipantEntity>(entity =>                    // PARTICIPANT
                {
                    entity.ToTable("Participant");                                      // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Participant_Id");              // PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                     .IsRowVersion()                                                    // SÄKERHET
                     .IsConcurrencyToken()                                              // SÄKERHET
                     .IsRequired(false);                                                     // SÄKERHET

                    entity.Property(e => e.FirstName)                                   // FÖRNAMN
                    .HasMaxLength(100)                                                  // MAX ANTAL TECKEN FÖR FÖRNAMN
                    .IsRequired();                                                      // FÖRNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.LastName)                                    // EFTERNAMN
                    .HasMaxLength(100)                                                  // MAX ANTAL TECKEN FÖR EFTERNAMN
                    .IsRequired();                                                      // EFTERNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.Email)                                       // EMAIL
                    .HasMaxLength(250)                                                  // MAX ANTAL TECKEN FÖR EMAIL
                    .IsRequired();                                                      // EMAIL ÄR OBLIGATORISKT

                    entity.HasIndex(e => e.Email)
                    .IsUnique();

                    entity.Property(e => e.PhoneNumber)                                 // PHONENUMBER
                    .HasMaxLength(15)                                                   // MAX ANTAL TECKEN FÖR NUMMER
                    .IsUnicode(false)                                                   // TAR BORT SPECIALTECKEN
                    .IsRequired();                                                      // TELEFONNUMMER ÄR OBLIGATORISKT

                    entity.Property(e => e.CreatedAt)                                   // LAS NYLIGEN TILL. TID. 
                    .IsRequired();



                    //TILLAGT. La till extra properties i klassen Participant.
                    entity.Property(e => e.UpdatedAt)
                    .IsRequired(false);


                    //Relationer - kan vara att jag ändrar sen
                    entity.HasMany(p => p.Enrollments)
                    .WithOne(e => e.Participant)
                    .HasForeignKey(e => e.ParticipantId);


                });




                    //TEACHER
                    modelBuilder.Entity<TeacherEntity>(entity =>                        // TEACHER
                {
                    entity.ToTable("Teacher");                                          // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Teacher_Id");                  // PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired(false);                                                      // SÄKERHET

                    entity.Property(e => e.FirstName)                                   // FÖRNAMN
                    .HasMaxLength(100)                                                  // MAX ANTAL TECKEN FÖR FÖRNAMN
                    .IsRequired();                                                      // FÖRNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.LastName)                                    // EFTERNAMN
                    .HasMaxLength(100)                                                  // MAX ANTAL TECKEN FÖR EFTERNAMN
                    .IsRequired();                                                      // EFTERNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.Email)                                       // EMAIL
                    .HasMaxLength(250)                                                  // MAX ANTAL TECKEN FÖR EMAIL
                    .IsRequired();                                                      // EMAIL ÄR OBLIGATORISKT

                    entity.HasIndex(e => e.Email)
                    .IsUnique();

                    entity.Property(e => e.PhoneNumber)                                 // PHONENUMBER
                    .HasMaxLength(15)                                                   // MAX ANTAL TECKEN FÖR NUMMER
                    .IsUnicode(false)                                                   // TAR BORT SPECIALTECKEN
                    .IsRequired();                                                      // TELEFONNUMMER ÄR OBLIGATORISKT

                    entity.Property(e => e.Major)                                       // MAJOR / SKOLÄMNE
                    .HasMaxLength(100)                                                  // MAX ANTAL TECKEN FÖR ÄMNE
                    .IsRequired();                                                      // ÄMNE ÄR OBLIGATORISKT

                    entity.Property(e => e.CreatedAt)
                    .IsRequired();


                    //TILLAGT. La till extra properties i klassen Teacher.
                    entity.Property(e => e.UpdatedAt)
                    .IsRequired(false);


                    //RELATIONER kANSKE behöver ändra denna. Just nu kan fler lärare ha samma kurs
                    entity.HasMany(t => t.CourseSessions)
                    .WithMany(cs => cs.Teachers);



                });




    }
}







