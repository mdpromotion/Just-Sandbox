using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    private AudioSource _audioSource;
    [SerializeField] private AudioDatabase _database;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        if (_audioSource == null)
            return;

        var clip = _database.GetClip(soundName);
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        else
        {
            Debug.LogError($"Sound '{soundName}' not found in AudioDatabase.");
        }
    }

    public void PlayOneShot(string soundName, float volume = 1f)
    {
        if (_audioSource == null)
            return;

        var clip = _database.GetClip(soundName);
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogError($"Sound {soundName} not found in database");
        }
    }

    public float Pitch
    {
        get => _audioSource.pitch;
        set => _audioSource.pitch = value;
    }
}