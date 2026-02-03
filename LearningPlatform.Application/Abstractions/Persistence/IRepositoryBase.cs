
namespace LearningPlatform.Application.Abstractions.Persistence;

public interface IRepositoryBase<TModel, in TKey>

{

    //Skapa sen alla klasserna med separata interfaces, nedan är bara det Hans visade i sin video
    //INTERFACE
    //CRUD
    Task<TModel?> GetByIdAsync(TKey id, CancellationToken ct = default);

    Task <IReadOnlyList<TModel>> ListAsync(CancellationToken ct = default);

    Task AddAsync(TModel model, CancellationToken ct = default);

    Task UpdateAsync(TModel model, CancellationToken ct = default);

    Task DeleteAsync(TModel model, CancellationToken ct = default);

}
