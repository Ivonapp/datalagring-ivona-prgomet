using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using LearningPlatform.Application.Teachers.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class TeacherServiceTest
{
    private readonly Mock<ITeacherRepository> repo = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private TeacherService CreateService() => new(repo.Object, uow.Object);

    [Fact]
    public async Task Create_ShouldReturnId()
    {
        var service = CreateService();
        var input = new TeacherInput("John", "Doe", "john@example.com", "123456789", "Math");

        repo.Setup(r => r.EmailAlreadyExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        repo.Setup(r => r.AddAsync(It.IsAny<TeacherModel>(), It.IsAny<CancellationToken>()))
            .Callback<TeacherModel, CancellationToken>((m, ct) =>
            {
                typeof(TeacherModel).GetProperty("Id")!.SetValue(m, 1); // Sätter Id i test
            })
            .Returns(Task.CompletedTask);

        var id = await service.CreateAsync(input);

        Assert.Equal(1, id);
    }

    [Fact]
    public async Task GetById_ShouldReturnTeacherOutput()
    {
        var service = CreateService();
        var model = new TeacherModel(1, "John", "Doe", "john@example.com", "123456789", "Math", Array.Empty<byte>(), DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("John", result!.FirstName);
    }

    [Fact]
    public async Task List_ShouldReturnAllTeachers()
    {
        var service = CreateService();
        var models = new List<TeacherModel>
        {
            new(1, "John", "Doe", "john@example.com", "123456789", "Math", Array.Empty<byte>(), DateTime.UtcNow, null),
            new(2, "Jane", "Smith", "jane@example.com", "987654321", "Physics", Array.Empty<byte>(), DateTime.UtcNow, null)
        };

        repo.Setup(r => r.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var result = await service.ListAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task Update_ShouldCallRepository()
    {
        var service = CreateService();
        var input = new TeacherInput("John", "Doe", "john@example.com", "123456789", "Math");
        var existing = new TeacherModel(1, "Old", "Name", "old@example.com", "000", "Physics", Array.Empty<byte>(), DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        await service.UpdateAsync(1, input);

        repo.Verify(r => r.UpdateAsync(It.Is<TeacherModel>(m => m.Id == 1 && m.FirstName == "John"), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldCallRepository()
    {
        var service = CreateService();
        var existing = new TeacherModel(1, "John", "Doe", "john@example.com", "123456789", "Math", Array.Empty<byte>(), DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        await service.DeleteAsync(1);

        repo.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
