using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Participants.Outputs;

public class ParticipantOutput
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}