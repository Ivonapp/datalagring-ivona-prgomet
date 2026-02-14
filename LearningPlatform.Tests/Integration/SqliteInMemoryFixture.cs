using LearningPlatform.Infrastructure.EFC.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Integration;

// HANS KOD
public sealed class SqliteInMemoryFixture : IAsyncLifetime
{
    private SqliteConnection? _conn;

    public DbContextOptions<InfrastructureDbContext> Options { get; private set; } = default!;

    public async Task InitializeAsync()
    {

        _conn = new SqliteConnection("Data Source=:memory:;Cache=Shared");
        await _conn.OpenAsync();

        Options = new DbContextOptionsBuilder<InfrastructureDbContext>()
            .UseSqlite(_conn)
            .EnableSensitiveDataLogging()
            .Options;

        await using var db = new InfrastructureDbContext(Options);
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {

        if (_conn is not null)
        {
            await _conn.DisposeAsync();
        }
    }

    public InfrastructureDbContext CreatedDbContext() => new(Options);
}

[CollectionDefinition(SqliteInMemoryCollection.Name)]
public sealed class SqliteInMemoryCollection : ICollectionFixture<SqliteInMemoryFixture>
{
    public const string Name = "SqliteInMemoryCollection";
}