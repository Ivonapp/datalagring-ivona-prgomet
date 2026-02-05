using LearningPlatform.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Abstractions.Persistence.Repositories;

public interface ITeacherRepository : IRepositoryBase<TeacherDto, int>
{
}
 