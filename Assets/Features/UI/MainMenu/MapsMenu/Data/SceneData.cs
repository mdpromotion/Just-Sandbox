namespace Feature.MapsMenu.Data
{
    public readonly struct SceneData
    {
        public readonly string Name;
        public readonly string Path;

        public SceneData(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}