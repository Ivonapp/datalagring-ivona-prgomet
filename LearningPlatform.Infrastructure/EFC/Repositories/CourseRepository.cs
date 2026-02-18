using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


//PÅGÅENDE 

namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class CourseRepository(InfrastructureDbContext Context) : EfcRepositoryBase<CourseEntity, int, CourseModel>(Context), ICourseRepository
    {


        //TOMODEL
        //FÖLJ MALLEN I MODEL
                public override CourseModel ToModel(CourseEntity entity) => new(
                entity.Id,
                entity.CourseCode,
                entity.Concurrency,
                entity.Title,
                entity.Description,
                entity.CreatedAt,
                entity.UpdatedAt,
                entity.TeacherId
                );


        //ADD
        //INGEN ID, UPDATEDAT, CONCURRENCY
        public override async Task AddAsync(CourseModel model, CancellationToken ct = default)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            // Validate teacher exists to avoid FK violation
            if (model.TeacherId <= 0 || !await Context.Set<TeacherEntity>().AnyAsync(t => t.Id == model.TeacherId, ct))
                throw new ArgumentException($"Teacher {model.TeacherId} not found.");

            var entity = new CourseEntity
            {
                TeacherId = model.TeacherId,
                CourseCode = model.CourseCode,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.UtcNow

            };

            await Set.AddAsync(entity, ct);               // Lägger till i kön - (utan denna och koden nedan händer ingenting.)
        }


            //UPDATE

            public override async Task UpdateAsync(CourseModel model, CancellationToken ct = default)
            {
                var entity = await Set.SingleOrDefaultAsync(x => x.Id == model.Id, ct)
                ?? throw new ArgumentException($"Course {model.Id} not found.");

                Context.Entry(entity).Property(x => x.Concurrency).OriginalValue = model.Concurrency;


                // 1. DATA SOM SKA GÅ ATT ÄNDRA!
                entity.CourseCode = model.CourseCode;
                entity.Title = model.Title;
                entity.Description = model.Description;

                // 2. TIDSTÄMPEL FÖR ÄNDRINGEN!
                entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}







