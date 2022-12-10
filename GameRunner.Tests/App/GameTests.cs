using FluentAssertions;
using GameRunner.App;
using Xunit;
using Xunit.Abstractions;

namespace GameRunner.Tests.App
{
    public class GameTests
    {
        private readonly ITestOutputHelper _output;

        public GameTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldRunGame()
        {
            var sut = CreateSut().Run(@"TestData\map2.txt");

            sut.Should().Be(13);

            _output.WriteLine("Game runs! Expected result: {0}", 13);
        }

        private Game CreateSut() =>
            new Game();
    }
}