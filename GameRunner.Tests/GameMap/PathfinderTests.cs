using FluentAssertions;
using GameRunner.GameMap;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace GameRunner.Tests.GameMap
{
    public class PathfinderTests
    {
        private readonly ITestOutputHelper _output;

        public PathfinderTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory, MemberData(nameof(PathFinderTestData))]
        public void ShouldFindStepsToNearestExit(
            Map map,
            int expected)
        {
            var sut = CreateSut(map, '1').GetSteps();

            sut.Should().Be(expected);

            _output.WriteLine("Steps: {0}", expected);
        }

        public static IEnumerable<object[]> PathFinderTestData =>
            new List<object[]>
            {
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', 'X', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', ' ', '1' }
                        },
                        Entrance = new List<int>() { 1, 2 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 4, 3 }
                        }
                    },
                    4
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', 'X', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', ' ', '1' }
                        },
                        Entrance = new List<int>() { 1, 2 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 0, 2 },
                            new List<int> { 4, 3 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', ' ', 'X', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 0, 2 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 0, 1 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { 'X', ' ', '1', '1', '1' },
                            new List<char>() { ' ', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 0, 0 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 0, 1 },
                            new List<int> { 1, 0 },
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', '1', '1', 'X' },
                            new List<char>() { '1', ' ', ' ', ' ', ' ' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 0, 4 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 1, 4 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { 'X', ' ', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 4, 0 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 4, 1 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', ' ', 'X' }
                        },
                        Entrance = new List<int>() { 4, 4 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 4, 3 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { ' ', ' ', ' ', ' ', ' ' },
                            new List<char>() { ' ', ' ', ' ', 'X', ' ' },
                            new List<char>() { ' ', ' ', ' ', ' ', ' ' },
                            new List<char>() { ' ', ' ', ' ', ' ', ' ' },
                            new List<char>() { ' ', ' ', ' ', ' ', ' ' }
                        },
                        Entrance = new List<int>() { 1, 3 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 0, 0 },
                            new List<int> { 0, 1 },
                            new List<int> { 0, 2 },
                            new List<int> { 0, 3 },
                            new List<int> { 0, 4 },
                            new List<int> { 1, 0 },
                            new List<int> { 1, 4 },
                            new List<int> { 2, 0 },
                            new List<int> { 2, 4 },
                            new List<int> { 3, 0 },
                            new List<int> { 3, 4 },
                            new List<int> { 4, 0 },
                            new List<int> { 4, 1 },
                            new List<int> { 4, 2 },
                            new List<int> { 4, 3 },
                            new List<int> { 4, 4 }
                        }
                    },
                    1
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', ' ', ' ', '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1', ' ', '1', '1', '1', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1', ' ', ' ', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', ' ', '1', ' ', '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1', 'X', ' ', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', '1', '1', '1', '1', '1', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1', ' ', ' ', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', '1', '1', ' ', '1', ' ', '1', '1', '1', ' ', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', '1', ' ', '1', ' ', ' ', ' ', '1' },
                            new List<char>() { '1', ' ', '1', '1', '1', ' ', '1', '1', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 5, 5 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 10, 1 },
                            new List<int> { 10, 5 }
                        }
                    },
                    13
                },
                new object[] {
                    new Map()
                    {
                        MapLayout = new List<List<char>>()
                        {
                            new List<char>() { '1', ' ', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', '1' },
                            new List<char>() { '1', '1', ' ', '1', ' ', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', '1', '1', ' ', '1', ' ', '1', '1', '1', '1', '1', '1', '1', '1', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1', '1', '1', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', '1', ' ', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', '1', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', '1', 'X', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1', '1', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', ' ', ' ', ' ', ' ', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', '1', '1', ' ', '1', ' ', '1', '1', '1', '1', '1', '1', '1', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', '1', ' ', '1', ' ', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', ' ', '1', '1' },
                            new List<char>() { '1', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '1', '1' },
                            new List<char>() { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' },
                            new List<char>() { '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1', '1' }
                        },
                        Entrance = new List<int>() { 9, 10 },
                        Exits = new List<List<int>>()
                        {
                            new List<int> { 0, 1 }
                        }
                    },
                    170
                }
            };

        private PathFinder CreateSut(Map map, char obstacle) =>
            new PathFinder(map, obstacle);
    }
}