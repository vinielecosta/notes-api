using Application.Features.GroupRequests;
using Application.Handlers.GroupRequestHandlers;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NotesApp.Domain.Entities;
using NotesApp.Domain.Interfaces;
using NSubstitute;

namespace Tests.Application.GroupTests;

public class UpdateGroupRequestHandlerTests
{
    public class CreateGroupRequestHandlerTest
    {
        private readonly IGroupRepository _repositoryMock;
        private readonly IValidator<UpdateGroupRequest> _validatorMock;
        private readonly Group.GroupBuilder _groupBuilder;
        private readonly UpdateGroupRequestHandler _sut;

        public CreateGroupRequestHandlerTest()
        {
            _repositoryMock = Substitute.For<IGroupRepository>();
            _validatorMock = Substitute.For<IValidator<UpdateGroupRequest>>();
            _groupBuilder = new Group.GroupBuilder();

            _sut = new UpdateGroupRequestHandler
                (
                _repositoryMock,
                _validatorMock,
                _groupBuilder
                );
        }

        [Fact]
        public async Task UpdateGroup_WithValidData_ReturnValidGroup()
        {
            var userId = 1L;
            var groupId = 1L;

            // DTO a ser enviado como parâmetro do método
            var groupDto = new UpdateGroupRequest { Name = "Jhon Doe", Description = "Dev" };

            // Retorno esperado
            var createdGroup = _groupBuilder
                .SetName("Jhon Doe")
                .SetDescription("Dev")
                .SetCreatorId(1L)
                .Build();

            var validationResult = new ValidationResult();

            // Obrigamos o resultado de validação mockado a retornar uma validação de sucesso
            _validatorMock.Validate(groupDto).Returns(validationResult);
            _repositoryMock.ExistingGroup(1L).Returns(createdGroup);
            _repositoryMock.UpdateGroup(Arg.Is<Group>(g => g.Name == "Jhon Doe" && g.Description == "Dev"), 1L, 1L).Returns(createdGroup);

            //Act
            var call = await _sut.Handle(groupDto, CancellationToken.None);

            call.Should().BeEquivalentTo(createdGroup);
            await _repositoryMock.Received(1).UpdateGroup(Arg.Is<Group>(g => g.Name == "Jhon Doe" && g.Description == "Dev"), 1L, 1L);

        }

        [Fact]
        public async Task UpdateGroup_WithInvalidData_ThrowsException()
        {
            var userId = 1L;
            var groupId = 1L;

            var groupDto = new UpdateGroupRequest { Name = "", Description = "Dev" };

            var validationFailure = new List<ValidationFailure>();
            var valitadionResult = new ValidationResult(validationFailure);

            _validatorMock.Validate(groupDto).Returns(valitadionResult);

            var call = Assert.ThrowsAsync<ValidationException>(async () => await _sut.Handle(groupDto, CancellationToken.None));

            await _repositoryMock.DidNotReceive().UpdateGroup(Arg.Is<Group>(g => g.Name == "Jhon Doe" && g.Description == "Dev"), userId, groupId);
        }


    }
}
