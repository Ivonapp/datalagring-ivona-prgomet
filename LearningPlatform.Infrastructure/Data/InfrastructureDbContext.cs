

using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.Infrastructure.Data;

public sealed class InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
