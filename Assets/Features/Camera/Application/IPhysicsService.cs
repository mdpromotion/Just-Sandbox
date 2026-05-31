namespace Features.Camera.Application
{
    public interface IPhysicsService
    {
        void ApplyPitch(float pitch);
        void ApplyFOV(float fov);
    }
}