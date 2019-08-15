using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Options;
using DakarRally.Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DakarRally.RaceSimulator.UnitTests.Models
{
    public class Simulator_Create
    {
        [Fact]
        public void HappyPath_RaceInserted()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RaceRepository.Insert(It.IsAny<Race>()));
            var rallyOptions = Options.Create(new RallyOptions());
            int raceYear = 2019;
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act
            int raceId = sut.Create(raceYear);

            //assert
            mockUnitOfWork.Verify(r => r.RaceRepository.Insert(It.IsAny<Race>()), Times.Once);
        }
    }
}