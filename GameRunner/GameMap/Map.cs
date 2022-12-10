namespace GameRunner.GameMap
{
    public record Map
    {
        public List<List<char>> MapLayout { get; set; } = new();

        public List<int> Entrance { get; set; } = new();

        public List<List<int>> Exits { get; set; } = new();
    }
}