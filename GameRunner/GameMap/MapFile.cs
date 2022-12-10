namespace GameRunner.GameMap
{
    public class MapFile
    {
        private readonly string _filePath;

        public MapFile(string path)
        {
            _filePath = path;
        }
            
        public string[] Read()
        {
            try
            {
                return File.ReadAllLines($"{_filePath}");
            }
            catch
            {
                throw new FileNotFoundException("Please select correct file path.");
            }
        }
            
    }
}