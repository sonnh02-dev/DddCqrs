using Castle.Components.DictionaryAdapter.Xml;
using DddCqrs.Application.Abstractions.Data;
using DddCqrs.Application.Features.Users.Commands.Create;
using DddCqrs.Application.Features.Users.Queries;
using DddCqrs.Domain.Users;
using DddCqrs.SharedKernel;
using FluentAssertions;
using NSubstitute;

namespace DddCqrs.Application.UnitTests.Users;

public class CreateUserCommandTests
{
    private static readonly CreateUserCommand Command = new("test@test.com", "FullName", true);

    private readonly CreateUserCommandHandler _handler;

    private readonly IUserRepository _userRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateUserCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new CreateUserCommandHandler(
            _userRepositoryMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsInvalid()
    {
        // Arrange
        CreateUserCommand invalidCommand = Command with { Email = "invalid_email" };

        // Act
        Result<Guid> result = await _handler.Handle(invalidCommand, default);

        // Assert
        result.Error.Should().Be(EmailErrors.InvalidFormat);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenEmailIsNotUnique()
    {
        // Arrange
        // Nếu gọi hàm IsEmailUniqueAsync với email test @test.com, thì trả về false.
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(false);


        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.EmailNotUnique);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenCreateSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallRepository_WhenCreateSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        // Act
        Result<Guid> result = await _handler.Handle(Command, default);

        // Assert
        //Kiểm tra xem Insert() có được gọi 1 lần với User có Email bằng test @test.com hay không.
        _userRepositoryMock.Received(1).Insert(Arg.Is<User>(u => u.Id == result.Value));


    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenCreateSucceeds()
    {
        // Arrange
        _userRepositoryMock.IsEmailUniqueAsync(Arg.Is<Email>(e => e.Value == Command.Email))
            .Returns(true);

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
