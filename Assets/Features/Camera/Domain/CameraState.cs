namespace Feature.PlayerCamera.Domain
{
    /// <summary>
    /// Represents the state of a camera, including its orientation in terms of yaw and pitch angles.
    /// </summary>
    /// <remarks>This class provides properties to access the camera's yaw and pitch, which define its
    /// rotation around the vertical and horizontal axes, respectively. The values are expressed in degrees.</remarks>
    public class CameraState : IReadOnlyCameraState
    {
        public float Yaw { get; set; }
        public float Pitch { get; set; }
    }
}