using Shared.Providers;
using UnityEngine;

namespace Features.Core.Infrastructure.Providers
{
    public class RandomProvider : IRandomProvider
    {
        public float NextFloat() => Random.value;
    }
}