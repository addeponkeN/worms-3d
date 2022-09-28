using UnityEngine;

namespace AudioSystem
{
    public static class AudioExtensions
    {
        public static void PlaySfx(this AudioSource source, string name)
        {
            if(AudioManager.Sfx.TryGetValue(name, out var audio))
            {
                if(source.isPlaying)
                    source.Stop();
                source.PlayOneShot(audio);
                Debug.Log("play sfx: {name");
            }
            else
            {
                Debug.LogWarning($"sfx '{name}' does not exit");
            }
        }
    }
}