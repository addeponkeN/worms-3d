using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Util;

namespace AudioSystem
{
    public static class AudioManager
    {
        private const string MusicFolderPath = "Sound/Music/";
        private const string SfxFolderPath = "Sound/Sfx/";

        public static Dictionary<string, AudioClip> AudioClips => _audioClips;
        // public static Dictionary<string, AudioClip> Music => _music;

        public static float SfxScaledVolume => GameSettings.SfxVolume.Value * GameSettings.MasterVolume.Value;
        public static float MusicScaledVolume => GameSettings.MusicVolume.Value * GameSettings.MasterVolume.Value;

        // private static Dictionary<string, AudioClip> _music;
        private static Dictionary<string, AudioClip> _audioClips;
        private static AudioPlayer _musicPlayer;
        private static GameObject _audioSourceContainer;
        private static Stack<AudioPlayer> _sources;
        private static List<AudioPlayer> _allPlayers;

        private static string GetFullMusicPath() => $"{Application.dataPath}/{MusicFolderPath}";
        private static string GetFullSfxPath() => $"{Application.dataPath}/{SfxFolderPath}";

        public static void Load()
        {
            _sources = new Stack<AudioPlayer>();
            _allPlayers = new List<AudioPlayer>();
            _audioSourceContainer = new GameObject("AudioSourcePool");
            Object.DontDestroyOnLoad(_audioSourceContainer);

            // _music = new Dictionary<string, AudioClip>();
            _audioClips = new Dictionary<string, AudioClip>();

            // ###################################################################
            //  todo - prettier abstract file loading system (mby later..)
            // ###################################################################
            var musicFiles = Directory.GetFiles(GetFullMusicPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < musicFiles.Length; i++)
            {
                string path = musicFiles[i];
                var filename = FileHelper.GetFilenameFromPath(path);
                var name = FileHelper.GetNameFromFilename(filename).Split('_', 2)[1];
                string finalPath = $"Assets/{MusicFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _audioClips.Add(name, clip);
            }

            var sfxFiles = Directory.GetFiles(GetFullSfxPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < sfxFiles.Length; i++)
            {
                string path = sfxFiles[i];
                var filename = FileHelper.GetFilenameFromPath(path);
                var name = FileHelper.GetNameFromFilename(filename);
                string finalPath = $"Assets/{SfxFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _audioClips.Add(name, clip);
            }

            // Debug.Log($"Loaded {_music.Count} songs");
            Debug.Log($"Loaded {_audioClips.Count} audio clips");
            Debug.Log("Audio loaded");

            GameSettings.MasterVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
            GameSettings.SfxVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
            GameSettings.MusicVolume.OnChangedEvent += OnAnyVolumeChangedEvent;
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
            source.volume = MusicScaledVolume;
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
            source.volume = SfxScaledVolume;
            source.Play();
            return source;
        }
    }
}