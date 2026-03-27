namespace Feature.Storage.Domain
{
    public enum GraphicsQuality
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Ultra = 3
    }
    public class GraphicsSettings : IReadOnlyGraphicsSettings
    {
        public GraphicsQuality GraphicsQuality { get; private set; }
        public GraphicsSettings(GraphicsQuality graphicsQuality = GraphicsQuality.Medium)
        {
            SetGraphicsQuality(graphicsQuality);
        }
        public void SetGraphicsQuality(GraphicsQuality quality)
        {
            GraphicsQuality = quality;
        }
    }
}