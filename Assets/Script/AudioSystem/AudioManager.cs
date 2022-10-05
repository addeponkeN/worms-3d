using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace AudioSystem
{
    public static class AudioManager
    {
        public static float ScaledSfxVolume => GameSettings.Get.SfxVolume.Value * GameSettings.Get.MasterVolume.Value;
        public static float ScaledMusicVolume => GameSettings.Get.MusicVolume.Value * GameSettings.Get.MasterVolume.Value;

        private static Dictionary<string, AudioClip> _audioClips;
        private static AudioPlayer _musicPlayer;
        private static GameObject _audioSourceContainer;
        private static Stack<AudioPlayer> _sources;
        private static List<AudioPlayer> _allPlayers;

        public static void Load()
        {
            _sources = new Stack<AudioPlayer>();
            _allPlayers = new List<AudioPlayer>();
            _audioClips = new Dictionary<string, AudioClip>();
            
            //  audio pool object
            _audioSourceContainer = new GameObject("AudioSourcePool");
            Object.DontDestroyOnLoad(_audioSourceContainer);

            var loadedAudioClips = Resources.LoadAll<AudioClip>("Sound/");

            const string musicPrefix = "music_";
            
            for (int i = 0; i < loadedAudioClips.Length; i++)
            {
                var clip = loadedAudioClips[i];
                var finalName = clip.name.StartsWith(musicPrefix) 
                    ? clip.name.Remove(0, musicPrefix.Length) 
                    : clip.name;

                _audioClips.Add(finalName, clip);
            }

            Debug.Log($"Loaded {_audioClips.Count} audio clips");

            GameSettings.Get.MasterVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
            GameSettings.Get.SfxVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
            GameSettings.Get.MusicVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
        }

        private static void OnAnyVolumeChangedEvent()
        {
            for (int i = 0; i < _allPlayers.Count; i++)
            {
                _allPlayers[i].UpdateVolume();
            }
        }

        internal static void ReturnAudioPlayer(AudioPlayer player)
        {
            player.GetSource().Stop();
            _sources.Push(player);
        }

        public static AudioClip GetSoundClip(string name)
        {
            if (_audioClips.TryGetValue(name, out var clip))
            {
                return clip;
            }

            return null;
        }

        private static AudioPlayer GetAudioPlayer()
        {
            AudioPlayer retSource;
            if (_sources.Count <= 0)
            {
                var go = new GameObject($"AudioPlayer{_allPlayers.Count}", typeof(AudioPlayer));
                go.transform.parent = _audioSourceContainer.transform;
                retSource = go.GetComponent<AudioPlayer>();
                _allPlayers.Add(retSource);
            }
            else
            {
                retSource = _sources.Pop();
            }

            retSource.GetSource().loop = false;
            return retSource;
        }

        private static AudioPlayer GetAudioPlayerSource(string name)
        {
            if (_audioClips.TryGetValue(name, out var clip))
            {
                var player = GetAudioPlayer();
                player.gameObject.name = name;
                player.IsMusic = false;
                var source = player.GetSource();
                source.clip = clip;
                source.loop = false;
                return player;
            }

            return null;
        }

        public static void PlayMusic(string musicName)
        {
            if (_musicPlayer == null)
            {
                _musicPlayer = GetAudioPlayerSource(musicName);
                _musicPlayer.IsMusic = true;
            }
            else
            {
                _musicPlayer.SetAudioClip(musicName);
            }

            var source = _musicPlayer.GetSource();
            source.volume = ScaledMusicVolume;
            source.loop = true;
            source.Play();
        }

        public static AudioSource PlaySfx(string sfxName)
        {
            var player = GetAudioPlayerSource(sfxName);

            if (player == null)
            {
                Debug.LogWarning($"sfx '{sfxName}' does not exit");
                return null;
            }

            var source = player.GetSource();
            source.volume = ScaledSfxVolume;
            source.Play();
            return source;
        }
    }
}