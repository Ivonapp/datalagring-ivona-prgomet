using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Infrastructure.EFC.Data;

namespace LearningPlatform.Infrastructure.EFC.UnitOfWork;

public class EfcUnitOfWork(InfrastructureDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);

}
