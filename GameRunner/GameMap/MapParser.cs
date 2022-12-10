namespace GameRunner.GameMap
{
    public class MapParser
    {
        private readonly char _entryPoint;
        private readonly char _obstacle;
        private readonly char _path;
        private readonly string[] _mapFile;

        public MapParser(string[] mapFile, char entryPoint, char obstacle, char path)
        {
            _mapFile = mapFile;
            _entryPoint = entryPoint;
            _obstacle = obstacle;
            _path = path;
        }
       
        public ParseResult<Map> CreateMap()
        {
            var mapLayout = ParseMapLayout(_mapFile);
            var entrance = ParseEntrance(mapLayout);
            var exit = ParseExit(mapLayout, entrance);

            if (IsValidMap(mapLayout, entrance, exit) is false)
                return new ParseResult<Map>(false, new());

            return new ParseResult<Map>(true, new()
            {
                MapLayout = mapLayout,
                Entrance = entrance,
                Exits = exit
            });
        }

        private List<List<char>> ParseMapLayout(string[] _mapFile)
        {
            var validation = new ParseResult<List<List<char>>>(false, new());
            var mapLayout = new List<List<char>>();
            
            foreach (var line in _mapFile)
            {
                if (IsValidSize(line) is false)
                    return validation.Result;
                
                var nodes = line.ToCharArray().ToList();

                if (IsValidNode(nodes) is false)
                    return validation.Result;

                mapLayout.Add(nodes);
            }
            return mapLayout;
        }

        private List<int> ParseEntrance(List<List<char>> mapLayout)
        {
            var validation = new ParseResult<List<int>>(false, new());

            if(mapLayout.Any() is false)
                return validation.Result;

            var entrance = new List<int>();
            
            foreach (var coordinate in mapLayout)
            {
                if (coordinate.Contains(_entryPoint))
                {
                    entrance.Add(mapLayout.IndexOf(coordinate));
                    entrance.Add(coordinate.IndexOf(_entryPoint));
                }
                if (entrance.Count > 2)
                    return validation.Result;
            }
            return entrance;
        }

        private List<List<int>> ParseExit(List<List<char>> mapLayout, List<int> entrance)
        {
            var validation = new ParseResult<List<List<int>>>(false, new());
            if (mapLayout.Any() is false || entrance.Any() is false)
                return validation.Result;

            var exit = new List<List<int>>();
            var rows = mapLayout.Count - 1;
            var columns = mapLayout[0].Count - 1;

            for (int row = 0; row <= rows; row++)
            {
                for (int column = 0; column <= columns; column++)
                {
                    if ((row == 0 ||
                        column == 0 ||
                        row == rows ||
                        column == columns) &&
                        mapLayout[row][column] != _obstacle &&
                        (row == entrance[0] && column == entrance[1]) is false)
                    {
                        exit.Add(new List<int> { row, column });
                    }
                }
            }
            return exit;
        }

        private bool IsValidMap(List<List<char>> mapLayout, List<int> entrance, List<List<int>> exit)
        {
            if (mapLayout.Any() is false ||
                entrance.Any() is false ||
                exit.Any() is false ||
                exit.Count > 1000)
                return false;

            return true;
        }

        private bool IsValidSize(string node)
        {
            if (node.Length != _mapFile[0].Length ||
                node.Length < 5 ||
                node.Length > 11000 ||
                _mapFile.Length < 5 ||
                _mapFile.Length > 11000)
                return false;

            return true;
        }

        private bool IsValidNode(List<char> coordinate)
        {
            if(coordinate.Any(c => c != _entryPoint && c != _obstacle && c != _path))
                return false;

            return true;
        }
            
    }
}