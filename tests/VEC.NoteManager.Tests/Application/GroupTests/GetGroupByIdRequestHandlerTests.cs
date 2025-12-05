using Application.Features.UserRequests.GroupRequests;
using Application.Handlers.GroupRequestHandlers;
using FluentValidation;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Entities;
using NSubstitute;

namespace Tests.Application.GroupTests;

public class GetGroupByIdRequestHandlerTests
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

    //     [Fact]
    //     public async Task GetGroupById_WithValidId_ReturnGroup()
    //     {
    //         //Arrange
    //         var userId = 1L;
    //         var groupId = 1L;
    //
    //         var existingGroup = _groupBuilder
    //             .SetName("Jhon Doe")
    //             .SetDescription("Dev")
    //             .SetCreatorId(1L)
    //             .Build();
    //
    //         _repositoryMock.GetGroupById(1L, 1L).Returns(existingGroup);
    //
    //         //Act
    //         var call = await _sut.GetGroupById(userId, groupId);
    //
    //         //Assert
    //         call.Should().BeEquivalentTo(existingGroup); // -> Garantindo que o retorno da chamada será o "existingGroup"
    //         await _repositoryMock.Received(1).GetGroupById(1L, 1L);
    //     }
    //
    //     [Fact]
    //     public async Task GetGroupById_WithInvalidId_ReturnGroup()
    //     {
    //         // Arrange
    //         var userId = 0;
    //         var groupId = 1L;
    //
    //         //Act
    //         var call = Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetGroupById(userId, groupId));
    //
    //         //Assert
    //         await _repositoryMock.DidNotReceive().GetGroupById(0, 1L);
    //     }

    }
}
