using UnityEngine;

namespace Feature.Storage.Domain
{
    public interface IReadOnlyAudioSettings
    {
        bool IsMusicEnabled { get; }
    }
}