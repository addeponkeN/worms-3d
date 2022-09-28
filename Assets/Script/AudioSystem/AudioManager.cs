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

        public static float MasterVolume { get; set; } = 0.5f;
        public static float SfxVolume { get; set; } = 1f;
        public static float MusicVolume { get; set; } = 1f;

        public static Dictionary<string, AudioClip> Sfx => _sfx;

        private static float SfxScaledVolume => SfxVolume * MasterVolume;
        private static float MusicScaledVolume => MusicVolume * MasterVolume;
        
        private static int _sourceCount;
        private static Dictionary<string, AudioClip> _music;
        private static Dictionary<string, AudioClip> _sfx;
        private static AudioSource _musicSource;
        private static GameObject _audioSourceContainer;
        private static Stack<AudioPlayer> _sources;

        private static string GetFullMusicPath() => $"{Application.dataPath}/{MusicFolderPath}";
        private static string GetFullSfxPath() => $"{Application.dataPath}/{SfxFolderPath}";

        public static void Load()
        {
            _sources = new Stack<AudioPlayer>();
            _audioSourceContainer = new GameObject("AudioSourcePool");
            Object.DontDestroyOnLoad(_audioSourceContainer);

            _music = new Dictionary<string, AudioClip>();
            _sfx = new Dictionary<string, AudioClip>();


            // ###################################################################
            //  todo - prettier abstract file loading system (mby later..)
            // ###################################################################
            var musicFiles = Directory.GetFiles(GetFullMusicPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for(int i = 0; i < musicFiles.Length; i++)
            {
                string path = musicFiles[i];
                var filename = FileHelper.GetFilenameFromPath(path);
                var name = FileHelper.GetNameFromFilename(filename).Split('_', 2)[1];
                string finalPath = $"Assets/{MusicFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _music.Add(name, clip);
            }

            var sfxFiles = Directory.GetFiles(GetFullSfxPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for(int i = 0; i < sfxFiles.Length; i++)
            {
                string path = sfxFiles[i];
                var filename = FileHelper.GetFilenameFromPath(path);
                var name = FileHelper.GetNameFromFilename(filename);
                string finalPath = $"Assets/{SfxFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _sfx.Add(name, clip);
            }

            Debug.Log($"Loaded {_music.Count} songs");
            Debug.Log($"Loaded {_sfx.Count} sfx");
            Debug.Log("Audio loaded");
        }

        internal static void ReturnAudioPlayer(AudioPlayer player)
        {
            _sources.Push(player);
        }

        private static AudioSource GetSource()
        {
            if(_sources.Count <= 0)
            {
                var go = new GameObject($"player{_sourceCount++}", typeof(AudioPlayer));
                go.transform.parent = _audioSourceContainer.transform;
                return go.GetComponent<AudioPlayer>().GetSource();
            }

            var source = _sources.Pop().GetSource();
            source.loop = false;
            return source;
        }

        public static void PlayMusic(string musicName)
        {
            if(_music.TryGetValue(musicName, out var clip))
            {
                if(_musicSource != null && _musicSource.isPlaying)
                    _musicSource.Stop();
                _musicSource = GetSource();
                _musicSource.clip = clip;
                _musicSource.loop = true;
                _musicSource.Play();
            }
            else
            {
                Debug.LogWarning($"music '{musicName}' does not exit");
            }
        }

        public static AudioSource PlaySfx(string sfxName)
        {
            if(_sfx.TryGetValue(sfxName, out var clip))
            {
                var source = GetSource();
                source.clip = clip;
                source.Play();
                return source;
            }

            Debug.LogWarning($"sfx '{sfxName}' does not exit");
            return null;
        }
    }
}