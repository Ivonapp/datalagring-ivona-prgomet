using LearningPlatform.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Data;

//PÅGÅENDE KOD. Denna är inte färdig. Försöker få ihop nån sorts struktur som jag förstår.
public sealed class InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : DbContext(options)
{
   

    //OnModelCreating = bestämmer hur tabellerna ska se ut i databasen
    //hHär nedan definierar vi HUR strukturen ska se ut i självaste DATABASEN: (Hans video 24:46 Använd kodbaserad modell för SQL-databas med Entity Framework Core (Code first)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



                    // COURSE 

                    modelBuilder.Entity<CourseEntity>(entity =>                         // COURSEENTITY
                {
                    entity.ToTable("CourseEntity");                                     // KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Course_Id");                   // PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired();                                                      // SÄKERHET

                    entity.Property(e => e.Title)                                       // TITLE
                    .HasMaxLength(100)
                    .IsRequired();

                     entity.Property(e => e.Description)                                // DESCRIPTION
                     .HasMaxLength(500)
                     .IsRequired(false);
                });





                    // COURSE SESSION

                    modelBuilder.Entity<CourseSessionEntity>(entity =>                 // COURSESESSIONENTITY
                {
           
                    entity.ToTable("CourseSessionEntity");                             //< KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_CourseSession_Id");           //PRIMARY KEY

                    entity.Property(e => e.CourseId)
                   .IsRequired();

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired();                                                      // SÄKERHET

                    entity.Property(e => e.StartDate)                                   //START DATUM
                    .IsRequired();

                    entity.Property(e => e.EndDate)                                     //SLUT DATUM
                    .IsRequired();

                });




                    // ENROLLMENT

                    modelBuilder.Entity<EnrollmentEntity>(entity =>                     //ENROLLMENTENTITY
                {

                    entity.ToTable("EnrollmentEntity");                                 //KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Enrollment_Id");               //PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired();                                                      // SÄKERHET

                    entity.Property(e => e.EnrollmentDate)                              //ENROLLMENTDATE
                    .IsRequired();

                });



                    //PARTICIPANT

                    modelBuilder.Entity<ParticipantEntity>(entity =>                    //PARTICIPANTENTITY
                {
                    entity.ToTable("ParticipantEntity");                                //KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Participant_Id");              //PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                     .IsRowVersion()                                                    // SÄKERHET
                     .IsConcurrencyToken()                                              // SÄKERHET
                     .IsRequired();                                                     // SÄKERHET

                    entity.Property(e => e.FirstName)                                   //FÖRNAMN
                    .HasMaxLength(100)                                                  //MAX ANTAL TECKEN FÖR FÖRNAMN
                    .IsRequired();                                                      //FÖRNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.LastName)                                    //EFTERNAMN
                    .HasMaxLength(100)                                                  //MAX ANTAL TECKEN FÖR EFTERNAMN
                    .IsRequired();                                                      //EFTERNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.Email)                                       //EMAIL
                    .HasMaxLength(250)                                                  //MAX ANTAL TECKEN FÖR EMAIL
                    .IsRequired();                                                      //EMAIL ÄR OBLIGATORISKT

                    entity.Property(e => e.PhoneNumber)                                 //PHONENUMBER
                    .HasMaxLength(15)                                                   //MAX ANTAL TECKEN FÖR NUMMER
                    .IsUnicode(false)                                                   //TAR BORT SPECIALTECKEN
                    .IsRequired();                                                      //TELEFONNUMMER ÄR OBLIGATORISKT
                });




                    //TEACHER
                    modelBuilder.Entity<TeacherEntity>(entity =>                        // TEACHERENTITY
                {
                    entity.ToTable("TeacherEntity");                                    //KLASSNAMN. namnet inuti ("Course") kommer bli namnet inuti databasen.
                    entity.HasKey(e => e.Id).HasName("Pk_Teacher_Id");                  //PRIMARY KEY

                    entity.Property(e => e.Concurrency)                                 // SÄKERHET
                    .IsRowVersion()                                                     // SÄKERHET
                    .IsConcurrencyToken()                                               // SÄKERHET
                    .IsRequired();                                                      // SÄKERHET

                    entity.Property(e => e.FirstName)                                   //FÖRNAMN
                    .HasMaxLength(100)                                                  //MAX ANTAL TECKEN FÖR FÖRNAMN
                    .IsRequired();                                                      //FÖRNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.LastName)                                    //EFTERNAMN
                    .HasMaxLength(100)                                                  //MAX ANTAL TECKEN FÖR EFTERNAMN
                    .IsRequired();                                                      //EFTERNAMN ÄR OBLIGATORISKT

                    entity.Property(e => e.Email)                                       //EMAIL
                    .HasMaxLength(250)                                                  //MAX ANTAL TECKEN FÖR EMAIL
                    .IsRequired();                                                      //EMAIL ÄR OBLIGATORISKT

                    entity.Property(e => e.PhoneNumber)                                 //PHONENUMBER
                    .HasMaxLength(15)                                                   //MAX ANTAL TECKEN FÖR NUMMER
                    .IsUnicode(false)                                                   //TAR BORT SPECIALTECKEN
                    .IsRequired();                                                      //TELEFONNUMMER ÄR OBLIGATORISKT

                    entity.Property(e => e.Major)                                       //PHONENUMBER
                    .HasMaxLength(15)                                                   //MAX ANTAL TECKEN FÖR NUMMER
                    .IsRequired();                                                      //TELEFONNUMMER ÄR OBLIGATORISKT
                });



    }
}







