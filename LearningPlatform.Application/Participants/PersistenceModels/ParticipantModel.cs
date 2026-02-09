using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Participants.PersistenceModels;


public sealed record ParticipantModel( //ParticipantModel skriver du i IParticipantModel.cs
int Id, //int är NYCKELN som du skriver efter ParticipantModel i IParticipantModel.cs
byte[] Concurrency,
string FirstName,
string LastName,
string Email,
string PhoneNumber,
DateTime CreatedAt,
DateTime? UpdatedAt);

// Denna koden har mindre boilerplate, samt hanteras inte känsliga uppgifter här (tror jag ?)

//Det är DENNA modellen vi ska använda oss av i "ITeacherRepository.cs" !*/
