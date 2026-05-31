namespace Features.Audio.Interfaces
{
    public interface IAudioPlayer
    {
        public void PlayOneShot(string soundName, float volume = 1f);
    }
}
