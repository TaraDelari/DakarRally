using DakarRally.Core.Models;
using Xunit;

namespace DakarRally.Common.UnitTests.Models
{
    public class Race_Start
    {
        [Fact]
        public void HappyPath_StatusRunning()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);

            //act
            sut.Start();

            //assert
            Assert.Equal(RaceStatus.Running, sut.RaceStatus);
            Assert.Equal(sut.StartTime, sut.UpdateTime);
        }
    }
}