using Shared.Providers;
using UnityEngine;

namespace Core.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public float Now => Time.time;
    }
}