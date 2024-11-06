using Application.Command;
using FluentAssertions;
using Shared.Messages;
using System;
using Xunit;

public class LocacaoCriarComandTests
{
    [Fact]
    public void Constructor_ShouldAddNotification_WhenMotoIdIsZero()
    {
        // Arrange
        var command = new LocacaoCriarComand(0, DateTime.Today, DateTime.Today.AddDays(1), 1, 1);

        // Act
        var hasNotification = command.Notifications.Count > 0;

        // Assert
        hasNotification.Should().BeTrue();
        command.Notifications.Should().ContainSingle(n => n.Message == "Moto não informado!");
    }

    [Fact]
    public void Constructor_ShouldAddNotification_WhenPlanoIdIsZero()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today, DateTime.Today.AddDays(1), 0, 1);

        // Act
        var hasNotification = command.Notifications.Count > 0;

        // Assert
        hasNotification.Should().BeTrue();
        command.Notifications.Should().ContainSingle(n =>  n.Message == "Plano não informado!");
    }

    [Fact]
    public void Constructor_ShouldAddNotification_WhenEntregadorIdIsZero()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today, DateTime.Today.AddDays(1), 1, 0);

        // Act
        var hasNotification = command.Notifications.Count > 0;

        // Assert
        hasNotification.Should().BeTrue();
        command.Notifications.Should().ContainSingle(n => n.Message == "Entregador não informado!");
    }

    [Fact]
    public void Constructor_ShouldAddNotification_WhenDataInicioIsInFuture()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today.AddDays(1), DateTime.Today.AddDays(2), 1, 1);

        // Act
        var hasNotification = command.Notifications.Count > 0;

        // Assert
        hasNotification.Should().BeTrue();
        command.Notifications.Should().ContainSingle(n =>  n.Message == "Data de início inválida!");
    }

    [Fact]
    public void Constructor_ShouldAddNotification_WhenDataPrevisaoTerminoIsBeforeDataInicio()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today, DateTime.Today.AddDays(-1), 1, 1);

        // Act
        var hasNotification = command.Notifications.Count > 0;

        // Assert
        hasNotification.Should().BeTrue();
        command.Notifications.Should().ContainSingle(n =>  n.Message == "Data de previsão de término deve ser após a data de início!");
    }

    [Fact]
    public void Constructor_ShouldNotAddNotification_WhenAllFieldsAreValid()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today, DateTime.Today.AddDays(1), 1, 1);

        // Act
        var isValid = command.IsValid;

        // Assert
        isValid.Should().BeTrue();
        command.Notifications.Should().BeEmpty();
    }

    [Fact]
    public void Map_ShouldReturnAlugarMotoCreated_WithCorrectValues()
    {
        // Arrange
        var command = new LocacaoCriarComand(1, DateTime.Today, DateTime.Today.AddDays(7), 1, 1);

        // Act
        var result = command.Map();

        // Assert
        result.Should().NotBeNull();
        result.MotoId.Should().Be(1);
        result.EntregadorId.Should().Be(1);
        result.PlanoId.Should().Be(1);
        result.DataInicio.Should().Be(DateTime.Today);
        result.DataPrevisaoTermino.Should().Be(DateTime.Today.AddDays(7));
    }
}
