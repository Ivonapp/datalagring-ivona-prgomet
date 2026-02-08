
namespace LearningPlatform.Application.Abstractions.Persistence;



public interface IRepositoryBase<TModel, in TKey>

{
    // ETT "BAS-INTERFACE" för ALLA klasser, som gör att man slipper upprepa kod.
    //CRUD
    Task<TModel?> GetByIdAsync(TKey id, CancellationToken ct = default);
    Task <IReadOnlyList<TModel>> ListAsync(CancellationToken ct = default);
    Task AddAsync(TModel model, CancellationToken ct = default);
    Task UpdateAsync(TModel model, CancellationToken ct = default);
    Task DeleteAsync(TKey id, CancellationToken ct = default);

}




