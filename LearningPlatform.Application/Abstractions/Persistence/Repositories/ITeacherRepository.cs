using LearningPlatform.Application.Teachers.PersistenceModels;
namespace LearningPlatform.Application.Abstractions.Persistence.Repositories;



public interface ITeacherRepository : IRepositoryBase<TeacherModel, int>
{
    Task<bool> EmailAlreadyExistsAsync(string email, CancellationToken ct = default); // > TeacherRepository
}

