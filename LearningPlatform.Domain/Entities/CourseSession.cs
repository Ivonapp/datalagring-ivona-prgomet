namespace LearningPlatform.Domain.Entities;

//PÅGÅENDE KOD
    public class CourseSession
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
    }
