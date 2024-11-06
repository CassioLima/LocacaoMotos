using Application.Command;
using Domain;
using Domain.Entity;
using Moq;
using Services;
using Shared.Messages;
using System;
using System.Collections.Generic;
using Xunit;
using images;
using Application;

public class EntregadorServiceTests
{
    private readonly Mock<IRepositoryBase<Entregador>> _repositoryMock;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<ImageSaver> _imageSaverMock;
    private readonly EntregadorService _entregadorService;

    public EntregadorServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryBase<Entregador>>();
        _notificationContextMock = new Mock<INotificationContext>();
        _imageSaverMock = new Mock<ImageSaver>();

        _entregadorService = new EntregadorService(
            _repositoryMock.Object,
            _notificationContextMock.Object);
    }

    [Fact]
    public void CreateEntregador_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var entregadorCreated = new EntregadorCreated
        {
            Nome = "", // Nome inválido
            CNPJ = "12345678000195",
            DataNascimento = DateTime.Today.AddYears(-25),
            NumeroCNH = "123456789",
            TipoCNH = "A",
            ImagemCNH = "imagem.jpg"
        };

        // Act
        _entregadorService.CreateEntregador(entregadorCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Entregador>()), Times.Never);
    }

    [Fact]
    public void CreateEntregador_ValidCommand_ShouldSaveEntregador()
    {
        // Arrange
        var entregadorCreated = new EntregadorCreated
        {
            Nome = "João",
            CNPJ = "12345678000195",
            DataNascimento = DateTime.Today.AddYears(-25),
            NumeroCNH = "123456789",
            TipoCNH = "A",
            ImagemCNH = "imagem.jpg"
        };

        _repositoryMock.Setup(r => r.Save(It.IsAny<Entregador>())).Returns(true);

        // Act
        _entregadorService.CreateEntregador(entregadorCreated);

        // Assert
        _repositoryMock.Verify(r => r.Save(It.IsAny<Entregador>()), Times.Once);
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Never);
    }

    [Fact]
    public void SendFoto_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var entregadorFotoCreated = new EntregadorFotoCreated
        {
            Id = 1,
            ImagemCNH = "" // Imagem inválida
        };

        // Act
        _entregadorService.SendFoto(entregadorFotoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Update(It.IsAny<Entregador>()), Times.Never);
    }

  
}
