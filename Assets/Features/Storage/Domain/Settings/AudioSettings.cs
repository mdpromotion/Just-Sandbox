
namespace Feature.Storage.Domain
{
    public class AudioSettings : IReadOnlyAudioSettings
    {
        public bool IsMusicEnabled { get; private set; }
        public AudioSettings(bool state = true)
        {
            ToggleMusic(state);
        }
        public void ToggleMusic(bool state)
        {
            IsMusicEnabled = state;
        }
    }
}