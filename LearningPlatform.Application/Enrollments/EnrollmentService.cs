using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.Outputs;
using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Application.Mappers;
using LearningPlatform.Application.Services;
using LearningPlatform.Domain.Entities;
using LearningPlatform.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Application.Enrollments;

public sealed class EnrollmentService
    (
    IEnrollmentRepository enrollment,
    IUnitOfWork uow
    ) : IEnrollmentService

{



    // CREATE
    public async Task<int> CreateAsync(EnrollmentInput input, CancellationToken ct = default)
    {

        //Osäker på om jag eventuellt ska lägga in något här angående kurs som söks (?) lämnar tomt for now.


    var enrollmentToCreate = new EnrollmentModel(
            0,
            Array.Empty<byte>(),
            DateTime.UtcNow,
            null,
            input.ParticipantId,
            input.CourseSessionId
        );

        await enrollment.AddAsync(enrollmentToCreate, ct);
        await uow.SaveChangesAsync(ct);

        return enrollmentToCreate.Id;
    }


    // DELETE
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var existing = await enrollment.GetByIdAsync(id, ct)
            ?? throw new ArgumentException("Enrollment not found");

        await enrollment.UpdateAsync(existing, ct);
        await enrollment.DeleteAsync(id, ct);
        await uow.SaveChangesAsync(ct);
    }


    // GET BY ID
    public async Task<EnrollmentOutput?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var model = await enrollment.GetByIdAsync(id, ct);
        return model is null ? null : EnrollmentMapper.ToOutput(model);
    }




    //              IREADONLYLIST
    public async Task<IReadOnlyList<EnrollmentOutput>> ListAsync(CancellationToken ct = default)
    {
        var models = await enrollment.ListAsync(ct);
        return EnrollmentMapper.ToOutputList(models); // LÄG TILL I MAPPER SOM MED DOM ANDRA
    }



    //              UPDATE
public async Task UpdateAsync(int id, EnrollmentInput input, CancellationToken ct = default)
    {
        var existing = await enrollment.GetByIdAsync(id, ct)
            ?? throw new ArgumentException($"Enrollment {id} not found.");

        var model = EnrollmentMapper.ToModel(input) with
        {
            Id = id,
            Concurrency = existing.Concurrency,
            UpdatedAt = DateTime.UtcNow 
        };

        await enrollment.UpdateAsync(model, ct);
        await uow.SaveChangesAsync(ct);
    }
}