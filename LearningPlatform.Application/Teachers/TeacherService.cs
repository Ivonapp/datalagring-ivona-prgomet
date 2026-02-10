using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Mappers;
using LearningPlatform.Application.Services;
using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using LearningPlatform.Application.Teachers.PersistenceModels;
using LearningPlatform.Domain.Entities.ValueObjects;



namespace LearningPlatform.Application.Teachers;

public sealed class TeacherService
    (
    ITeacherRepository teacher,
    IUnitOfWork uow
    ) : ITeacherService
{
    //              CREATE ASYNC //SE VALUEOBJECTS MAPPEN, den innehåller typ "Regex" för email och phone
public async Task<int> CreateAsync(TeacherInput input, CancellationToken ct = default)
            
    {

        var email = new Email(input.Email);
        var PhoneNumber = string.IsNullOrWhiteSpace(input.PhoneNumber)
            ? null
            : new PhoneNumber(input.PhoneNumber);

        if (await teacher.EmailAlreadyExistsAsync(email.Value, ct))
        {
            throw new ArgumentException("Teacher with this email already exists.");
        }

        //Skrivit denna i samma ordning som i PersistenceModels (TeacherModel)
        var teacherToCreate = new TeacherModel(
                0,
                input.FirstName,
                input.LastName,
                email.Value,
                input.PhoneNumber,
                input.Major,
                Array.Empty<byte>(),
                DateTime.UtcNow,
                null
            );

        await teacher.AddAsync(teacherToCreate, ct);
        await uow.SaveChangesAsync(ct);
        return teacherToCreate.Id;
    }



    //             DELETE ASYNC
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await teacher.GetByIdAsync(id, ct)
            ?? throw new ArgumentNullException("Teacher not found");

        await teacher.UpdateAsync(existing, ct);
        await teacher.DeleteAsync(id, ct);
        await uow.SaveChangesAsync(ct);
    }

    //              GET BY ID ASYNC
    public async Task<TeacherOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var model = await teacher.GetByIdAsync(id, ct);
        return model is null ? null : TeacherMapper.ToOutput(model);
    }


    //                  IREADONLYLIST 
    public async Task<IReadOnlyList<TeacherOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await teacher.ListAsync(ct);
        return TeacherMapper.ToOutputList(models);
    }


    //                  UPDATEASYNC
    public async Task UpdateAsync(int id, TeacherInput input, CancellationToken ct = default)
    {
        var existing = await teacher.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"Teacher with id {id} not found.");

        var email = new Email(input.Email);
        var model = TeacherMapper.ToModel(input) with
        {
            Id = id,
            Concurrency = existing.Concurrency,
            Email = email.Value
        };

        await teacher.UpdateAsync(model, ct);
        await uow.SaveChangesAsync(ct);
    }

}
