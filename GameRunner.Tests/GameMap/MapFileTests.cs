using FluentAssertions;
using GameRunner.GameMap;
using Xunit;
using Xunit.Abstractions;

namespace GameRunner.Tests.GameMap
{
    public class MapFileTests
    {
        private readonly ITestOutputHelper _output;

        public MapFileTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldReadMapFile()
        {
            var sut = CreateSut().Read();

            sut.Should().NotBeNull();

            _output.WriteLine("Not Null");
        }

        private MapFile CreateSut() =>
            new MapFile(@"TestData\map1.txt");
    }
}