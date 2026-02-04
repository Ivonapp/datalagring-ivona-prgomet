namespace LearningPlatform.Domain.Entities;

//PÅGÅENDE KOD
    public class CourseSession
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }     // När kursen börjar för studenten
        public DateTime EndDate { get; set; }       // När kursen slutar för studenten
        public DateTime CreatedAt { get; set; }     // Tidpunkt då sessionen skapades i databasen
        public DateTime? UpdatedAt { get; set; }    // Tidpunkt för sessionens senaste ändring

    }
