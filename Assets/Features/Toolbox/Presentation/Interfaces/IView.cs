using System;
using UnityEngine;

namespace Feature.Toolbox.Presentation
{
    public interface IView
    {
        event Action<SpawnButtonData> SpawnButtonClicked;
        event Action<int> TabButtonClicked;
        event Action<SpawnSettingsButtonData> SettingsButtonClicked;
        void ToggleToolbox(bool state);
        void ToggleTab(int index, bool active);
        void ToggleButton(int index, bool active);
        void ToggleInventorySpawnButton(bool active);
        void ToggleTextureSpawnButton(int index, bool active);
    }
}