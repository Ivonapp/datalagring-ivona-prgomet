
using LearningPlatform.Application.Abstractions;
using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace LearningPlatform.Infrastructure.Repositories;


// FÖLJDE HANS KOD NEDAN I HANS VIDEO DÅ HANS KOD VAR MER DEN STRUKTUREN JAG VILLE HA ÄN T EX EMILS KOD. 
// KOMMER GÅ IGENOM KODEN NEDAN MER NOGGRANT SENARE, DÅ DEN KUNDE VARA RÄTT SVÅR ATT FÖRSTÅ
//GÅR HAND I HAND MED IREPOSITORYBASE.CS
public abstract class EfcRepositoryBase<TEntity, TKey, TModel>(InfrastructureDbContext Context) : IRepositoryBase<TModel, TKey> where TEntity : class, IEntity<TKey>
{

    // KOPPLING TILL SJÄLVASTE DATABASDELEN (InfrastructureDbContext.cs)
    protected InfrastructureDbContext Context { get; } = Context;
    protected DbSet<TEntity> Set => Context.Set<TEntity>();




    // ABSTRAKTA DELAR
    public abstract TModel ToModel(TEntity entity);
    public abstract Task AddAsync(TModel model, CancellationToken ct = default);
    public abstract Task UpdateAsync(TModel model, CancellationToken ct = default);





    // GENERELLA HÄMTNINGS-DELAR

    // DELETE
    public virtual async Task DeleteAsync(TKey id, CancellationToken ct = default)
        {
        var entity = await Set.SingleOrDefaultAsync(x => x.Id!.Equals(id), ct);         //HÄMTAR EN ENTITET
        if (entity is null) return;

        Set.Remove(entity);
        await Context.SaveChangesAsync(ct); //OM JAG LÄGGER TILL UNITOFWORK SÅ SKA DENNA TAS BORT.
    }


    //
    public virtual async Task<TModel?> GetByIdAsync(TKey id, CancellationToken ct = default)
        {
        var entity = await Set.AsNoTracking().SingleOrDefaultAsync(x => x.Id!.Equals(id), ct);         //HÄMTAR EN ENTITET
        return entity is null ? default : ToModel(entity);
    }


    // 
    public virtual async Task<IReadOnlyList<TModel>> ListAsync(CancellationToken ct = default)
        {
        var entities = await Set.AsNoTracking().ToListAsync(ct);
        return [.. entities.Select(ToModel)];
        }

}


