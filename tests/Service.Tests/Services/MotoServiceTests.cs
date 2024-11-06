using Application;
using Application.Command;
using Domain;
using Domain.Entity;
using Moq;
using Services;
using Shared.Messages;
using System.Collections.Generic;
using Xunit;

public class LocacaoCommandHandlerTests
{
    private readonly Mock<IRepositoryBase<Moto>> _repositoryMock;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly MotoService _motoService;

    public LocacaoCommandHandlerTests()
    {
        _repositoryMock = new Mock<IRepositoryBase<Moto>>();
        _notificationContextMock = new Mock<INotificationContext>();
        _motoService = new MotoService(_repositoryMock.Object, _notificationContextMock.Object);
    }

    [Fact]
    public void CreateMoto_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var motoCreated = new MotoCreated
        {
            Placa = "",  // Placa inválida
            Marca = "Honda",
            Modelo = "CG 160",
            Ano = 2022
        };

        // Act
        _motoService.CreateMoto(motoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Moto>()), Times.Never);
    }

    [Fact]
    public void CreateMoto_ValidCommand_ShouldSaveMoto()
    {
        // Arrange
        var motoCreated = new MotoCreated
        {
            Placa = "ABC1234",
            Marca = "Honda",
            Modelo = "CG 160",
            Ano = 2022
        };

        _repositoryMock.Setup(r => r.Save(It.IsAny<Moto>())).Returns(true);

        // Act
        _motoService.CreateMoto(motoCreated);

        // Assert
        _repositoryMock.Verify(r => r.Save(It.IsAny<Moto>()), Times.Once);
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Never);
    }

    [Fact]
    public void DeleteMoto_ShouldDeleteMotoById()
    {
        // Arrange
        var motoDeleted = new MotoDeleted { Id = 1 };

        // Act
        _motoService.DeleteMoto(motoDeleted);

        // Assert
        _repositoryMock.Verify(r => r.Delete(motoDeleted.Id), Times.Once);
    }

    [Fact]
    public void UpdateMoto_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var motoUpdated = new MotoUpdated
        {
            Id = 1,
            Placa = ""  // Placa inválida
        };

        // Act
        _motoService.UpdateMoto(motoUpdated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.IsAny<Moto>()), Times.Never);
    }

    [Fact]
    public void UpdateMoto_ValidCommand_ShouldUpdateMoto()
    {
        // Arrange
        var motoUpdated = new MotoUpdated
        {
            Id = 1,
            Placa = "DEF5678"
        };

        var existingMoto = new Moto { Id = 1, Placa = "ABC1234", Marca = "Honda", Modelo = "CG 160", Ano = 2022 };

        _repositoryMock.Setup(r => r.GetById(motoUpdated.Id)).Returns(existingMoto);
        _repositoryMock.Setup(r => r.Update(It.IsAny<Moto>())).Returns(true);

        // Act
        _motoService.UpdateMoto(motoUpdated);

        // Assert
        _repositoryMock.Verify(r => r.Update(It.Is<Moto>(m => m.Placa == motoUpdated.Placa)), Times.Once);
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Never);
    }
}
