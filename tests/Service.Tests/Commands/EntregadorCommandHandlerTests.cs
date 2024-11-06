using Application;
using Application.Command;
using Domain;
using Domain.Entity;
using Flunt.Notifications;
using MassTransit;
using Moq;
using Shared.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class EntregadorCommandHandlerTests
{
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<IRepositoryBase<Entregador>> _repositoryMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly EntregadorCommandHandler _commandHandler;

    public EntregadorCommandHandlerTests()
    {
        _notificationContextMock = new Mock<INotificationContext>();
        _repositoryMock = new Mock<IRepositoryBase<Entregador>>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();

        _commandHandler = new EntregadorCommandHandler(
            _notificationContextMock.Object,
            _repositoryMock.Object,
            _publishEndpointMock.Object);
    }

    [Fact]
    public async Task Handle_EntregadorCriarComand_DuplicateCNH_ShouldReturnError()
    {
        // Arrange
        var request = new EntregadorCriarComand("João", "12345678000195", DateTime.Now.AddYears(-30), "123456", "A", "imagem.jpg");

        _repositoryMock.Setup(r => r.GetAll()).Returns(new List<Entregador> { new Entregador { NumeroCNH = "123456" } }.AsQueryable());

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        //Assert.Equal("Numero da CNH já cadastrada", result.Message);
    }

    [Fact]
    public async Task Handle_EntregadorCriarComand_ValidCommand_ShouldPublishEventAndReturnSuccess()
    {
        // Arrange
        var request = new EntregadorCriarComand("João", "12345678000195", DateTime.Now.AddYears(-30), "987654", "A", "imagem.jpg");

        _repositoryMock.Setup(r => r.GetAll()).Returns(new List<Entregador>().AsQueryable());
        _publishEndpointMock.Setup(p => p.Publish(It.IsAny<EntregadorCreated>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<EntregadorCreated>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_EntregadorFotoCriarComand_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var request = new EntregadorFotoCriarComand(1, ""); // Imagem CNH vazia

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<EntregadorFotoCreated>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_EntregadorFotoCriarComand_ValidCommand_ShouldPublishEventAndReturnSuccess()
    {
        // Arrange
        var request = new EntregadorFotoCriarComand(1, "base64ImageString");

        _publishEndpointMock.Setup(p => p.Publish(It.IsAny<EntregadorFotoCreated>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<EntregadorFotoCreated>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
