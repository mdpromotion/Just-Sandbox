using Shared.Data;

namespace Shared.Providers
{
    public interface IPlayerTransformController
    {
        void Teleport(Position3 position);
        void ResetAngle();
        void ApplyYaw(float yaw);
    }
}