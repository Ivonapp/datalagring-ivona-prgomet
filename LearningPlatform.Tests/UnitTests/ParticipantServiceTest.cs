using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using LearningPlatform.Application.Participants.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class ParticipantServiceTest
{
    private readonly Mock<IParticipantRepository> repo = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private ParticipantService CreateService() => new(repo.Object, uow.Object);

    [Fact]
    public async Task Create_ShouldReturnId()
    {
        var service = CreateService();
        var input = new ParticipantInput("John", "Doe", "john@example.com", "123456789");

        repo.Setup(r => r.EmailAlreadyExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        repo.Setup(r => r.AddAsync(It.IsAny<ParticipantModel>(), It.IsAny<CancellationToken>()))
            .Callback<ParticipantModel, CancellationToken>((m, ct) =>
            {
                typeof(ParticipantModel).GetProperty("Id")!.SetValue(m, 1); // Sätt Id i test
            })
            .Returns(Task.CompletedTask);

        var id = await service.CreateAsync(input);

        Assert.Equal(1, id);
    }

    [Fact]
    public async Task GetById_ShouldReturnParticipantOutput()
    {
        var service = CreateService();
        var model = new ParticipantModel(1, Array.Empty<byte>(), "John", "Doe", "john@example.com", "123456789", DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("John", result!.FirstName);
    }

    [Fact]
    public async Task List_ShouldReturnAllParticipants()
    {
        var service = CreateService();
        var models = new List<ParticipantModel>
        {
            new(1, Array.Empty<byte>(), "John", "Doe", "john@example.com", "123456789", DateTime.UtcNow, null),
            new(2, Array.Empty<byte>(), "Jane", "Smith", "jane@example.com", "987654321", DateTime.UtcNow, null)
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
        var input = new ParticipantInput("John", "Doe", "john@example.com", "123456789");
        var existing = new ParticipantModel(1, Array.Empty<byte>(), "Old", "Name", "old@example.com", "000", DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        await service.UpdateAsync(1, input);

        repo.Verify(r => r.UpdateAsync(It.Is<ParticipantModel>(m => m.Id == 1 && m.FirstName == "John"), It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Delete_ShouldCallRepository()
    {
        var service = CreateService();
        var existing = new ParticipantModel(1, Array.Empty<byte>(), "John", "Doe", "john@example.com", "123456789", DateTime.UtcNow, null);

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existing);

        await service.DeleteAsync(1);

        repo.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
