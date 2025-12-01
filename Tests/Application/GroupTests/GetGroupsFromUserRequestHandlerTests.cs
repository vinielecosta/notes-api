// namespace Tests.Application.GroupTests;
//
// public class GetGroupsFromUserRequestHandlerTests
// {
//     public class CreateGroupRequestHandlerTest
//     {
//         private readonly IGroupRepository _repositoryMock;
//         private readonly IValidator<CreateGroupRequest> _validatorMock;
//         private readonly Group.GroupBuilder _groupBuilder;
//         private readonly CreateGroupRequestHandler _sut;
//
//         public CreateGroupRequestHandlerTest()
//         {
//             _repositoryMock = Substitute.For<IGroupRepository>();
//             _validatorMock = Substitute.For<IValidator<CreateGroupRequest>>();
//             _groupBuilder = new Group.GroupBuilder();
//
//             _sut = new CreateGroupRequestHandler 
//                 (
//                 _repositoryMock,
//                 _groupBuilder,
//                 _validatorMock
//                 );
//         }
//     //
//     //     [Fact]
//     //     public async Task GetGroupsFromUser_WithValidId_ReturnGroups()
//     //     {
//     //         //Arrange
//     //         var userId = 1L;
//     //
//     //         var existingGroup = _groupBuilder
//     //           .SetName("Jhon Doe")
//     //           .SetDescription("Dev")
//     //           .SetCreatorId(1L)
//     //           .Build();
//     //
//     //         var expectedGroups = new List<Group> { existingGroup };
//     //
//     //         _repositoryMock.GetGroupsFromUser(1L).Returns(expectedGroups);
//     //
//     //         //Act
//     //         var call = await _sut.GetGroupsFromUser(userId);
//     //
    //         //Assert
    //         call.Should().BeEquivalentTo(expectedGroups);
    //         await _repositoryMock.Received(1).GetGroupsFromUser(1L);
    //     }
    //
    //     [Fact]
    //     public void GetGroupsFromUser_WithInvalidId_ShouldntReturnGroups()
    //     {
    //         //Arrange
    //         var userId = 0;
    //
    //         //Act
    //         var call = Assert.ThrowsAsync<ValidationException>(async () => await _sut.GetGroupsFromUser(userId));
    //
    //         //Assert
    //         _repositoryMock.DidNotReceive().GetGroupsFromUser(0);
    //     }
//
// }
