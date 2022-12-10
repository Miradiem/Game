namespace GameRunner.GameMap
{
    public class PathFinder
    {
        private readonly List<int[]> _directions = new List<int[]>()
        {
            new int[] {-1, 0},
            new int[] {1, 0},
            new int[] {0, -1},
            new int[] {0, 1}
        };
        private readonly List<List<char>> _map;
        private readonly List<int> _entrance;
        private readonly List<List<int>> _exits;
        private readonly char _obstacle;

        public PathFinder(Map map, char obstacle)
        {
            _map = map.MapLayout;
            _entrance = map.Entrance;
            _exits = map.Exits;
            _obstacle = obstacle;
        }

        public int GetSteps()
        {
            if (_map.Any() is false ||
                _entrance.Any() is false ||
                _exits.Any() is false)
                return 0;

            var rows = _map.Count - 1;
            var columns = _map[0].Count - 1;

            var positionQue = new Queue<List<int>>();
            var steps = 0;
            positionQue.Enqueue(new List<int> { _entrance[0], _entrance[1], steps });

            while (positionQue.Count > 0)
            {
                var currentPosition = positionQue.Dequeue();

                foreach (var exit in _exits)
                {
                    if (currentPosition[0] == exit[0] &&
                        currentPosition[1] == exit[1])
                        return currentPosition[2];
                }

                if (_map[currentPosition[0]][currentPosition[1]] == _obstacle)
                    continue;
                
                foreach (var direction in _directions)
                {
                    var nextStep = new List<int>
                    {
                        currentPosition[0] + direction[0],
                        currentPosition[1] + direction[1],
                        currentPosition[2] + 1
                    };

                    if (nextStep[0] >= 0 &&
                        nextStep[0] <= rows &&
                        nextStep[1] >= 0 &&
                        nextStep[1] <= columns &&
                        _map[nextStep[0]][nextStep[1]] != _obstacle)
                    {
                        _map[currentPosition[0]][currentPosition[1]] = _obstacle;
                        positionQue.Enqueue(nextStep);
                    }
                }
            }
            return 0;
        }
    }
}