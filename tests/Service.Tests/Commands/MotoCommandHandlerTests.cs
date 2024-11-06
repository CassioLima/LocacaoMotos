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

public class MotoCommandHandlerTests
{
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<IRepositoryBase<Moto>> _repositoryMock;
    private readonly Mock<IRepositoryBase<Locacao>> _repositoryLocacaoMock;
    private readonly MotoCommandHandler _commandHandler;

    public MotoCommandHandlerTests()
    {
        _notificationContextMock = new Mock<INotificationContext>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _repositoryMock = new Mock<IRepositoryBase<Moto>>();
        _repositoryLocacaoMock = new Mock<IRepositoryBase<Locacao>>();

        _commandHandler = new MotoCommandHandler(
            _notificationContextMock.Object,
            _publishEndpointMock.Object,
            _repositoryMock.Object,
            _repositoryLocacaoMock.Object);
    }

    [Fact]
    public async Task Handle_MotoCriarComand_InvalidCommand_ShouldAddNotification()
    {
        // Arrange
        var request = new MotoCriarComand("", "", "", 1800); // Dados inválidos

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        _notificationContextMock.Verify(n => n.AddNotification(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Once);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<MotoCreated>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_MotoRemoverComand_MotoHasLocacao_ShouldReturnError()
    {
        // Arrange
        var request = new MotoRemoverComand(1);

        _repositoryMock.Setup(r => r.GetById(request.Id)).Returns(new Moto { Id = request.Id });
        _repositoryLocacaoMock.Setup(r => r.GetAll()).Returns(new List<Locacao> { new Locacao { MotoId = request.Id } }.AsQueryable());

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task Handle_MotoRemoverComand_ValidCommand_ShouldPublishMotoDeletedEvent()
    {
        // Arrange
        var request = new MotoRemoverComand(1);

        _repositoryMock.Setup(r => r.GetById(request.Id)).Returns(new Moto { Id = request.Id });
        _repositoryLocacaoMock.Setup(r => r.GetAll()).Returns(new List<Locacao>().AsQueryable());
        _publishEndpointMock.Setup(p => p.Publish(It.IsAny<MotoDeleted>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<MotoDeleted>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_MotoAtualizarComand_MotoNotFound_ShouldReturnError()
    {
        // Arrange
        var request = new MotoAtualizarComand(1, "XYZ1234");

        _repositoryMock.Setup(r => r.GetById(request.Id)).Returns((Moto)null);

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public async Task Handle_MotoAtualizarComand_ValidCommand_ShouldPublishMotoUpdatedEvent()
    {
        // Arrange
        var request = new MotoAtualizarComand(1, "XYZ1234");

        _repositoryMock.Setup(r => r.GetById(request.Id)).Returns(new Moto { Id = request.Id, Placa = "ABC1234" });
        _publishEndpointMock.Setup(p => p.Publish(It.IsAny<MotoUpdated>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<MotoUpdated>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
