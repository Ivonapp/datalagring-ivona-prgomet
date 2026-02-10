using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Participants.Inputs;


// NY RECORD

public sealed record ParticipantInput(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber
);





// GAMLA KLASSEN NEDAN
/*public class ParticipantInput
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}*/



