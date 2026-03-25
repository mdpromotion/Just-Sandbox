using System;
using UnityEngine;

namespace Feature.Toolbox.Domain
{
    public interface IMenuEvents
    {
        event Action<bool> ToolboxToggled;
    }
}