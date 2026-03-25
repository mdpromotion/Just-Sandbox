using UnityEngine;

public interface IAudioPlayer
{
    public void Play(string soundName);
    public void PlayOneShot(string soundName, float volume = 1f);
    public float Pitch { get; set; }
}
