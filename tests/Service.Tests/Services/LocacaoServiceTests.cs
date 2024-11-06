using Application;
using Application.Command;
using Domain;
using Domain.Entity;
using Moq;
using Services;
using Shared.Messages;
using System;
using System.Collections.Generic;
using Xunit;

public class LocacaoServiceTests
{
    private readonly Mock<IRepositoryBase<Locacao>> _repositoryMock;
    private readonly Mock<IRepositoryBase<Plano>> _repositoryPlanoMock;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly LocacaoService _locacaoService;

    public LocacaoServiceTests()
    {
        _repositoryMock = new Mock<IRepositoryBase<Locacao>>();
        _repositoryPlanoMock = new Mock<IRepositoryBase<Plano>>();
        _notificationContextMock = new Mock<INotificationContext>();

        _locacaoService = new LocacaoService(
            _repositoryMock.Object,
            _notificationContextMock.Object,
            _repositoryPlanoMock.Object);
    }

    [Fact]
    public void AlugarMoto_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var alugarMotoCreated = new AlugarMotoCreated
        {
            MotoId = 1,
            DataInicio = DateTime.Now,
            DataPrevisaoTermino = DateTime.Now.AddDays(-1), // Data inválida (término antes do início)
            PlanoId = 1,
            EntregadorId = 1
        };

        // Act
        _locacaoService.AlugarMoto(alugarMotoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Locacao>()), Times.Never);
    }

    [Fact]
    public void AlugarMoto_InvalidPlanoId_ShouldAddNotification()
    {
        // Arrange
        var alugarMotoCreated = new AlugarMotoCreated
        {
            MotoId = 1,
            DataInicio = DateTime.Now,
            DataPrevisaoTermino = DateTime.Now.AddDays(7),
            PlanoId = -1, // PlanoId inválido
            EntregadorId = 1
        };

        // Act
        _locacaoService.AlugarMoto(alugarMotoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Locacao>()), Times.Never);
    }

    [Fact]
    public void AlugarMoto_EntregadorIdNotSet_ShouldAddNotification()
    {
        // Arrange
        var alugarMotoCreated = new AlugarMotoCreated
        {
            MotoId = 1,
            DataInicio = DateTime.Now,
            DataPrevisaoTermino = DateTime.Now.AddDays(7),
            PlanoId = 1,
            EntregadorId = 44 // EntregadorId inválido
        };

        // Act
        _locacaoService.AlugarMoto(alugarMotoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Locacao>()), Times.Never);
    }

    [Fact]
    public void AlugarMoto_InvalidDateRange_ShouldAddNotification()
    {
        // Arrange
        var alugarMotoCreated = new AlugarMotoCreated
        {
            MotoId = 1,
            DataInicio = DateTime.Now.AddDays(10), // Início após a data de criação
            DataPrevisaoTermino = DateTime.Now.AddDays(7), // Termina antes de começar
            PlanoId = 1,
            EntregadorId = 12
        };

        // Act
        _locacaoService.AlugarMoto(alugarMotoCreated);

        // Assert
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Flunt.Notifications.Notification>>()), Times.Once);
        _repositoryMock.Verify(r => r.Save(It.IsAny<Locacao>()), Times.Never);
    }
}
