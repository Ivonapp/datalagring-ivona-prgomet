using System;
using System.Collections.Generic;
using System.Text;


namespace LearningPlatform.Application.Teachers.PersistenceModels;


public sealed record TeacherModel( //TeacherModel skriver du i ITeacherRepository.cs
int Id, //int är NYCKELN som du skriver efter TeacherModel i ITeacherRepository.cs
string FirstName,
string LastName,
string Email,
string PhoneNumber,
string Major,
byte[] Concurrency,
DateTime CreatedAt,
DateTime? UpdatedAt);

// Denna koden har mindre boilerplate, samt hanteras inte känsliga uppgifter här (tror jag ?)

//Det är DENNA modellen vi ska använda oss av i "ITeacherRepository.cs" !