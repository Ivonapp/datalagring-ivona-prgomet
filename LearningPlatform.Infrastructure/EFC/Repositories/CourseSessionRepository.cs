using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class CourseSessionRepository(InfrastructureDbContext Context) : EfcRepositoryBase<CourseSessionEntity, int, CourseSessionModel>(Context), ICourseSessionRepository
    {
        public override Task AddAsync(CourseSessionModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public override CourseSessionModel ToModel(CourseSessionEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(CourseSessionModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}





