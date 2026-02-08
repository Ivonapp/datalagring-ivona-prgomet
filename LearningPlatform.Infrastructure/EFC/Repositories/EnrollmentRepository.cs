using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using System;
using System.Collections.Generic;
using System.Text;


//PÅGÅENDE


namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class EnrollmentRepository(InfrastructureDbContext Context) : EfcRepositoryBase<EnrollmentEntity, int, EnrollmentModel>(Context), IEnrollmentRepository
    {
        public override Task AddAsync(EnrollmentModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public override EnrollmentModel ToModel(EnrollmentEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(EnrollmentModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}


