using Shared.Providers;
using UnityEngine;

namespace Features.Core.Infrastructure.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public float Now => Time.time;
    }
}