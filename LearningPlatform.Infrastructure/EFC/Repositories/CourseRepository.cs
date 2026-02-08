using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using System;
using System.Collections.Generic;
using System.Text;


//PÅGÅENDE 

namespace LearningPlatform.Infrastructure.EFC.Repositories
{
    public class CourseRepository(InfrastructureDbContext Context) : EfcRepositoryBase<CourseEntity, int, CourseModel>(Context), ICourseRepository
    {
        public override Task AddAsync(CourseModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public override CourseModel ToModel(CourseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(CourseModel model, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}




