using Application.Features.UserRequests.GroupRequests;
using Application.Handlers.GroupRequestHandlers;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces;
using NSubstitute;

namespace Tests.Application.GroupTests
{
    public class CreateGroupRequestHandlerTest
    {
        private readonly IGroupRepository _repositoryMock;
        private readonly IValidator<CreateGroupRequest> _validatorMock;
        private readonly Group.GroupBuilder _groupBuilder;
        private readonly CreateGroupRequestHandler _sut;

        public CreateGroupRequestHandlerTest()
        {
            _repositoryMock = Substitute.For<IGroupRepository>();
            _validatorMock = Substitute.For<IValidator<CreateGroupRequest>>();
            _groupBuilder = new Group.GroupBuilder();

            _sut = new CreateGroupRequestHandler 
                (
                _repositoryMock,
                _groupBuilder,
                _validatorMock
                );
        }

        [Fact]
        public async Task CreateGroup_WithValidData_ShouldReturnCreatedUser()
        {
            // Arrange
            var userId = 1L;
            var createGroupRequest = new CreateGroupRequest { Name = "Jhon Doe", Description = "Dev", UserId = 1L };
            var validationResult = new ValidationResult();

            var createdGroup = _groupBuilder
                .SetName("Jhon Doe")
                .SetDescription("Dev")
                .SetCreatorId(1L)
                .Build();

            _validatorMock.Validate(createGroupRequest).Returns(validationResult);

            _repositoryMock.CreateGroup(Arg.Is<Group>(g => g.Name == "Jhon Doe" && g.Description == "Dev"), userId).Returns(createdGroup);

            // Act
            var call = await _sut.Handle(createGroupRequest, CancellationToken.None);

            // Assert
            call.Should().BeEquivalentTo(createdGroup);
            await _repositoryMock.Received(1).CreateGroup(Arg.Is<Group>(g => g.Name == "Jhon Doe" && g.Description == "Dev"), userId);
        }

        [Fact]
        public async Task CreateGroup_WithInvalidData_ShoudntReturnCreatedUser()
        {
            // Arrange
            var userId = 1L;
            var groupDto = new CreateGroupRequest { Name = "", Description = "Dev" };
            var validationFailure = new List<ValidationFailure> { new(groupDto.Name, "Name is required") };
            var validationResult = new ValidationResult(validationFailure);

            _validatorMock.Validate(groupDto).Returns(validationResult);

            // Act
            var call = Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(groupDto, CancellationToken.None));

            // Assert
            await _repositoryMock.DidNotReceive().CreateGroup(Arg.Is<Group>(g => g.Name == "" && g.Description == "Dev"), userId);
        }

    //
    //
    //
    //     [Fact]
    //     public async Task DeleteGroup_WithValidId_ShouldReturnDeletedGroup()
    //     {
    //         //Arrange 
    //         var userId = 1L;
    //         var groupId = 1L;
    //
    //         //Act
    //         await _sut.DeleteGroup(userId, groupId);
    //
    //         //Assert
    //         await _repositoryMock.Received(1).DeleteGroup(1L, 1L);
    //     }
    //
    //     [Fact]
    //     public async Task DeleteGroup_WithInvalidId_ShouldntReturnGroup()
    //     {
    //         //Arrange 
    //         var userId = 0L;
    //         var groupId = 1L;
    //         //Act
    //         var act = Assert.ThrowsAsync<ArgumentException>(async () => await _sut.DeleteGroup(userId, groupId));
    //         //Assert
    //         await _repositoryMock.Received(0).DeleteGroup(1L, 1L);
    //     }
    }
}
