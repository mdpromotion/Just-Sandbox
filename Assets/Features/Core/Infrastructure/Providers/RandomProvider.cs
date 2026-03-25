using Shared.Providers;
using UnityEngine;

namespace Core.Providers
{
    public class RandomProvider : IRandomProvider
    {
        public float NextFloat() => Random.value;
    }
}