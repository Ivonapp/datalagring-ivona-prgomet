using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.Outputs;
using LearningPlatform.Application.Enrollments.PersistenceModels;
using Moq;
using Xunit;

public sealed class EnrollmentServiceTest
{
    private readonly Mock<IEnrollmentRepository> repo = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private EnrollmentService CreateService() => new(repo.Object, uow.Object);

    // CREATE
    [Fact]
    public async Task Create_ShouldReturnId()
    {
        var service = CreateService();
        var input = new EnrollmentInput(101, 202);

        repo.Setup(r => r.AddAsync(It.IsAny<EnrollmentModel>(), It.IsAny<CancellationToken>()))
            .Callback<EnrollmentModel, CancellationToken>((m, ct) =>
            {
                var field = typeof(EnrollmentModel).GetProperty("Id")!;
                field.SetValue(m, 1);
            })
            .Returns(Task.CompletedTask);

        var id = await service.CreateAsync(input);

        Assert.Equal(1, id);
    }

    // READ BY ID
    [Fact]
    public async Task Read_ShouldReturnEnrollment()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EnrollmentModel(1, Array.Empty<byte>(), DateTime.UtcNow, null, 101, 202));

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(101, result!.ParticipantId);
        Assert.Equal(202, result.CourseSessionId);
    }

    // LIST
    [Fact]
    public async Task List_ShouldReturnEnrollments()
    {
        var service = CreateService();

        repo.Setup(r => r.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<EnrollmentModel>
            {
                new EnrollmentModel(1, Array.Empty<byte>(), DateTime.UtcNow, null, 101, 202)
            });

        var list = await service.ListAsync();

        Assert.Single(list);
    }

    // UPDATE
    [Fact]
    public async Task Update_ShouldModifyEnrollment()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EnrollmentModel(1, Array.Empty<byte>(), DateTime.UtcNow, null, 101, 202));

        var input = new EnrollmentInput(303, 404);

        await service.UpdateAsync(1, input);

        repo.Verify(r => r.UpdateAsync(It.Is<EnrollmentModel>(m =>
            m.Id == 1 &&
            m.ParticipantId == 303 &&
            m.CourseSessionId == 404
        ), It.IsAny<CancellationToken>()), Times.Once);

        uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // DELETE
    [Fact]
    public async Task Delete_ShouldRemoveEnrollment()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new EnrollmentModel(1, Array.Empty<byte>(), DateTime.UtcNow, null, 101, 202));

        await service.DeleteAsync(1);

        repo.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // DELETE 
    [Fact]
    public async Task Delete_ShouldThrow_WhenMissing()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(9, It.IsAny<CancellationToken>()))
            .ReturnsAsync((EnrollmentModel?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(9));
    }
}
