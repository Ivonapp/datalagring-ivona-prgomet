namespace LearningPlatform.Application.DTOs;

public class ParticipantDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; } //När användaren skapades

}


// TROR DESSA SKA RADERAS EFTERSOM DU NU HAR PERSISTENCE MODELS