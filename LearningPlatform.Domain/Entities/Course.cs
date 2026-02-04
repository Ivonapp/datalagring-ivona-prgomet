namespace LearningPlatform.Domain.Entities;

//PÅGÅENDE KOD
    public class Course
    {
        public int Id { get; set; }
        public int CourseCode { get; set; } //TILLAGD NY 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

}
