using UnityEngine;

public class ControlSettings : IReadOnlyControlSettings
{
    public int MouseSensitivity { get; private set; }
    public ControlSettings(int mouseSensitivity = 5)
    {
        SetMouseSensitivity(mouseSensitivity);
    }
    public void SetMouseSensitivity(int sensitivity)
    {
        if (sensitivity < 0)
            return;

        MouseSensitivity = sensitivity;
    }
}
