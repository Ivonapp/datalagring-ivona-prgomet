using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;









//PÅGÅENDE


namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class EnrollmentRepository(InfrastructureDbContext Context) : EfcRepositoryBase<EnrollmentEntity, int, EnrollmentModel>(Context), IEnrollmentRepository
    {



        //TOMODEL
        //FÖLJ MALLEN I MODEL
        public override EnrollmentModel ToModel(EnrollmentEntity entity) => new(
        entity.Id,
        entity.Concurrency,
        entity.EnrollmentDate,
        entity.UpdatedAt,
        entity.ParticipantId,
        entity.CourseSessionId
);




        //ADD
        //INGEN ID, UPDATEDAT, CONCURRENCY

        public override async Task AddAsync(EnrollmentModel model, CancellationToken ct = default)
        {
            var entity = new EnrollmentEntity
            {
                EnrollmentDate = DateTime.UtcNow,
                ParticipantId = model.ParticipantId,
                CourseSessionId = model.CourseSessionId
            };

            await Set.AddAsync(entity, ct);               // Lägger till i kön - (utan denna och koden nedan händer ingenting.)
        }




        //UPDATE

                public override async Task UpdateAsync(EnrollmentModel model, CancellationToken ct = default)
        {
            var entity = await Set.SingleOrDefaultAsync(x => x.Id == model.Id, ct)
                ?? throw new ArgumentException($"Enrollment {model.Id} not found.");

            Context.Entry(entity).Property(x => x.Concurrency).OriginalValue = model.Concurrency;

                entity.UpdatedAt = DateTime.UtcNow;
                entity.ParticipantId = model.ParticipantId;
                entity.CourseSessionId = model.CourseSessionId;
        }
    }
}


