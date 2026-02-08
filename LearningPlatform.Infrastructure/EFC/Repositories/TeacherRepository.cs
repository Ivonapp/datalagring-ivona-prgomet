using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Teachers.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LearningPlatform.Infrastructure.EFC.Repositories;


//FÄRDIG 

public class TeacherRepository(InfrastructureDbContext Context) : EfcRepositoryBase<TeacherEntity, int, TeacherModel>(Context), ITeacherRepository
{


            // MODEL - STÅR I EfcRepositoryBase
            // (MHÄMTAR UT LÄRARE, MATCHAR ALLT SOM STÅR I TEACHERMODEL)
            public override TeacherModel ToModel(TeacherEntity entity) => new(
                    entity.Id,
                    entity.FirstName,
                    entity.LastName,
                    entity.Email,
                    entity.PhoneNumber,
                    entity.Major,
                    entity.Concurrency,
                    entity.CreatedAt,
                    entity.UpdatedAt
                );



            // ADD - STÅR I EfcRepositoryBase
            // (KONTROLLERAR ATT ID INTE ÄR TOMT - OM LÄRARE ÄR TOM SÅ SKICKAS EXCEPTION UT)
            public override async Task AddAsync(TeacherModel model, CancellationToken ct = default)
            {
               if (model.Id == 0) //Chatgpt hjälpte mig med att det endast kan vara 0 för int, och inte "empty." som man gör med Guid.
               throw new ArgumentException("Teacher Id must be set by the application layer");


                var entity = new TeacherEntity
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Major = model.Major
                };

                await Set.AddAsync(entity, ct);               // Lägger till i kön - (utan denna och koden nedan händer ingenting.)
            }




            // UPDATE STÅR I EfcRepositoryBase
            public override async Task UpdateAsync(TeacherModel model, CancellationToken ct = default)
            {
                var entity = await Set.SingleOrDefaultAsync(x => x.Id == model.Id, ct)
                    ?? throw new ArgumentException($"Teacher {model.Id} not found.");

                Context.Entry(entity).Property(x => x.Concurrency).OriginalValue = model.Concurrency;

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email.Trim();
                entity.PhoneNumber = model.PhoneNumber;
                entity.Major = model.Major;
                entity.UpdatedAt = DateTime.UtcNow;
            }




            // EMAIL - Att se till att ingen kan registrera sig med en mejladress som redan är upptagen - INTERFACET
            // lägg till
            // Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
            // i ITeacherRepository interfacet (ENDAST DENNA SKA STÅ I INTERFACE då det ENDAST ska vara TEACHER som står där)

            public async Task<bool> EmailAlreadyExistsAsync(string email, CancellationToken ct = default)

                {
                var normalized = email.Trim();

                return await Set
                .AsNoTracking()
                .AnyAsync(x => x.Email == normalized, ct);
                }
            }

