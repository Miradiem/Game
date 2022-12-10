using GameRunner.GameMap;

namespace GameRunner.App;

public class Game : IGame
{
    public int Run(string filePath)
    {
        var file = new MapFile(filePath).Read();

        var map = new MapParser(file, 'X', '1', ' ').CreateMap().Result;

        return new PathFinder(map, '1').GetSteps();
    }
}