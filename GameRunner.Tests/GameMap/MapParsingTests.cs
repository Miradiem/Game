using FluentAssertions;
using GameRunner.GameMap;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GameRunner.Tests.GameMap
{
    public class MapParsingTests
    {
        private readonly ITestOutputHelper _output;

        public MapParsingTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory, MemberData(nameof(MapParsingTestData))]
        public void ShouldCreateMap(MapParser mapData)
        {
            var sut = mapData.CreateMap().Result;

            sut.MapLayout.Should().NotBeEmpty();
            sut.Entrance.Should().NotBeEmpty();
            sut.Exits.Should().NotBeEmpty();

            _output.WriteLine("Not Empty");
        }

        [Theory, MemberData(nameof(IncorrectMapParsingTestData))]
        public void ShouldCreateEmptyMap(MapParser mapData)
        {
            var sut = mapData.CreateMap().Result;

            sut.MapLayout.Should().BeEmpty();
            sut.Entrance.Should().BeEmpty();
            sut.Exits.Should().BeEmpty();

            _output.WriteLine("Empty");
        }

        public static IEnumerable<object[]> MapParsingTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1 1 1",
                            "1   1",
                            "111 1"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111111111",
                            "1     1   1",
                            "1 1 1 111 1",
                            "1 1 1     1",
                            "1 1 1 11111",
                            "1   1X    1",
                            "1 1111111 1",
                            "1   1     1",
                            "111 1 111 1",
                            "1   1 1   1",
                            "1 111 11111"
                        },
                        'X', '1', ' ')
                }
            };

        public static IEnumerable<object[]> IncorrectMapParsingTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1   1",
                            "1   1",
                            "111 "
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1   1",
                            "111 1"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1   1",
                            "1   1",
                            "11111"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1   1",
                            "1X  1",
                            "111 1"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string[]
                        {
                            "11111",
                            "1 X 1",
                            "1   1",
                            "1Y  1",
                            "111 1"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string []
                        {
                            "X1111",
                            "1   1",
                            "1   1",
                            "1   1",
                            "11111"
                        },
                        'X', '1', ' ')
                },
                new object[]
                {
                    new MapParser(
                        new string []
                        {
                            "11111",
                            "1   1",
                            "1   1",
                            "1   1",
                            "111 1"
                        },
                        'X', '1', ' ')
                }
            };
    }
}