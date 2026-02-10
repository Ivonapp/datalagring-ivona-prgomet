using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Participants.Outputs;


// NYA RECORD
public sealed record ParticipantOutput(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime CreatedAt
);



// GAMLA KLASSEN NEDAN
/*public class ParticipantOutput
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}*/