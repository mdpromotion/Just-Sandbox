using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/AudioDatabase")]

public class AudioDatabase : ScriptableObject
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Sound[] sounds;

    public AudioClip GetClip(string soundName)
    {
        foreach (var s in sounds)
        {
            if (s.name == soundName)
                return s.clip;
        }
        return null;
    }
}