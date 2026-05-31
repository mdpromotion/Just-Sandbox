namespace Features.Camera.Domain
{
    public interface IReadOnlyCameraState
    {
        float Yaw { get; }
        float Pitch { get; }
    }
}