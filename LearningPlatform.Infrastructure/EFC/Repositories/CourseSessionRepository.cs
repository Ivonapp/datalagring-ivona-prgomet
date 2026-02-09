using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class CourseSessionRepository(InfrastructureDbContext Context) : EfcRepositoryBase<CourseSessionEntity, int, CourseSessionModel>(Context), ICourseSessionRepository
    {
        //TOMODEL
        //FÖLJ MALLEN I MODEL
        public override CourseSessionModel ToModel(CourseSessionEntity entity) => new(
        entity.Id,
        entity.CourseId,
        entity.Concurrency,
        entity.StartDate,
        entity.EndDate,
        entity.CreatedAt,
        entity.UpdatedAt
        
);


        //ADD
        //INGEN ID, UPDATEDAT, CONCURRENCY

        public override async Task AddAsync(CourseSessionModel model, CancellationToken ct = default)
        {
            var entity = new CourseSessionEntity
            {


                CourseId = model.CourseId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CreatedAt = DateTime.UtcNow

            };

            await Set.AddAsync(entity, ct);               // Lägger till i kön - (utan denna och koden nedan händer ingenting.)
        }


        //UPDATE
        public override async Task UpdateAsync(CourseSessionModel model, CancellationToken ct = default)
        {
            var entity = await Set.SingleOrDefaultAsync(x => x.Id == model.Id, ct)
                ?? throw new ArgumentException($"CourseSession {model.Id} not found."); //JUSTERA

            Context.Entry(entity).Property(x => x.Concurrency).OriginalValue = model.Concurrency;


            // 1. DATA SOM SKA GÅ ATT ÄNDRA!
            entity.CourseId = model.CourseId;
            entity.StartDate = model.StartDate;
            entity.EndDate = model.EndDate;

            // 2. TIDSTÄMPEL FÖR ÄNDRINGEN!
            entity.UpdatedAt = DateTime.UtcNow;


        }
    }
}





