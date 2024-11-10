// ****************************************************************
//  Assembly         : CleanArchitecture.Api.UnitTests
//  Author           :  cmalagoncmalagon
//  Created          : 06-15-2024
//
//  Last Modified By : Carlos Fernando Malagón Cano
//  Last Modified On : 06-15-2024
//  ****************************************************************
//  <copyright file="PlayersControllerTest.cs"
//      company="Cafemaca - CAFEMACA Colombia">
//      Cafemaca - CAFEMACA Colombia
//  </copyright>
//

using CAFEMACA.Coink.PruebaTecnica.Api.Controllers.Players;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Abstractions.Interfaces.Services;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Dtos.Players;
using CAFEMACA.Coink.PruebaTecnica.Application.Common.Options;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Abstractions;
using CAFEMACA.Coink.PruebaTecnica.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace CAFEMACA.Coink.PruebaTecnica.Api.UnitTests.Controllers.Players
{
    public class PlayersControllerTest
    {
        private readonly Mock<IPlayerServices> _playerserviceMock;
        private readonly Mock<IOptions<ApplicationOptions>> _applicationOptionsMock;

        public PlayersControllerTest()
        {
            _playerserviceMock = new Mock<IPlayerServices>(MockBehavior.Strict);
            _applicationOptionsMock = new Mock<IOptions<ApplicationOptions>>(MockBehavior.Strict);
        }

        /// <summary>
        /// <MethodName>_should_<expectation>_when_<condition>
        /// nameOfMethodBeingTested_Scenario_ExpectedBehaviour
        /// </summary>
        [Fact]
        public async Task GetPlayerShouldGettheplayerWhenIdexistAsync()
        {
            // Arrange
            _playerserviceMock.Setup(x => x.SelectPlayerByIdAsync(1, default))
                .ReturnsAsync(new PlayerResponse
                {
                    Id = 1,
                    Name = "Test",
                    Password = "password",
                    Email = "email"
                }
                );

            // Act
            var controller = new PlayersController(_playerserviceMock.Object, _applicationOptionsMock.Object);
            var result = await controller.GetPlayer(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<PlayerResponse>(okResult.Value);
            Assert.Equal(1, item.Id);

        }

        [Fact]
        public async Task GetPlayerShouldGetErrorWhenIdNonexistAsync()
        {
            // Arrange
            _playerserviceMock.Setup(x => x.SelectPlayerByIdAsync(0, default))
                .ReturnsAsync(PlayerErrors.NotFound(0)
                );

            // Act
            var controller = new PlayersController(_playerserviceMock.Object, _applicationOptionsMock.Object);
            var result = await controller.GetPlayer(0);

            //Assert
            var errorResult = Assert.IsType<NotFoundObjectResult>(result);
            var item = Assert.IsType<DomainError>(errorResult.Value);
            Assert.Equal(PlayerErrors.NotFound(0).ErrorCode, item.ErrorCode);

        }
    }
}
