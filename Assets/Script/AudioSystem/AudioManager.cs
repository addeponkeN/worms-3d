using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Get { get; private set; }

        private const string MusicFolderPath = "Sound/Music/";
        private const string SfxFolderPath = "Sound/Sfx/";

        private static string GetFullMusicPath() => $"{Application.dataPath}/{MusicFolderPath}";
        private static string GetFullSfxPath() => $"{Application.dataPath}/{SfxFolderPath}";

        private static int _sourceCount;
        private static bool _loaded = false;
        private static Dictionary<string, AudioClip> _music;
        private static Dictionary<string, AudioClip> _sfx;

        public Dictionary<string, AudioClip> Sfx => _sfx;

        private AudioSource _musicSource;
        private GameObject _audioSourceContainer;   //  audiosource pool
        private Stack<AudioPlayer> _sources;

        private void Awake()
        {
            if(Get == null)
            {
                Get = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _sources = new Stack<AudioPlayer>();
            _audioSourceContainer = new GameObject("AudioSourceContainer");
            DontDestroyOnLoad(_audioSourceContainer);
            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
        }

        private void Start()
        {
            Load();
        }

        private static void Load()
        {
            if(_loaded)
                return;

            _loaded = true;

            _music = new Dictionary<string, AudioClip>();
            _sfx = new Dictionary<string, AudioClip>();

            
            // ###################################################################
            //  todo - create prettier abstract file loading system (mby later..)
            // ###################################################################
            
            var musicFiles = Directory.GetFiles(GetFullMusicPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for(int i = 0; i < musicFiles.Length; i++)
            {
                string path = musicFiles[i];
                var idx = path.LastIndexOf('/') + 1;
                var filename = path.Substring(idx, path.Length - idx);
                var name = filename.Split('_', 2)[1].Split('.', 2)[0];
                string finalPath = $"Assets/{MusicFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _music.Add(name, clip);
            }

            var sfxFiles = Directory.GetFiles(GetFullSfxPath(), "*.wav", SearchOption.TopDirectoryOnly);
            for(int i = 0; i < sfxFiles.Length; i++)
            {
                string path = sfxFiles[i];
                var idx = path.LastIndexOf('/') + 1;
                var filename = path.Substring(idx, path.Length - idx);
                var name = filename.Split('.', 2)[0];
                string finalPath = $"Assets/{SfxFolderPath}{filename}";
                var clip = AssetDatabase.LoadAssetAtPath<AudioClip>(finalPath);
                _sfx.Add(name, clip);
            }

            Debug.Log($"Loaded {_music.Count} songs");
            Debug.Log($"Loaded {_sfx.Count} sfx");
            Debug.Log("Audio loaded");
        }

        public void ReturnAudioPlayer(AudioPlayer player)
        {
            _sources.Push(player);
        }

        private AudioSource GetSource()
        {
            if(_sources.Count <= 0)
            {
                var go = new GameObject($"player{_sourceCount++}", typeof(AudioPlayer));
                go.transform.parent = _audioSourceContainer.transform;
                return go.GetComponent<AudioPlayer>().GetSource();
            }

            return _sources.Pop().GetSource();
        }

        public static void PlayMusic(string musicName)
        {
            var source = Get._musicSource;
            if(_music.TryGetValue(musicName, out var clip))
            {
                if(source.isPlaying)
                    source.Stop();
                source.clip = clip;
                source.Play();
            }
            else
            {
                Debug.LogWarning($"music '{musicName}' does not exit");
            }
        }

        public static AudioSource PlaySfx(string sfxName)
        {
            var source = Get.GetSource();
            if(_sfx.TryGetValue(sfxName, out var clip))
            {
                if(source.isPlaying)
                    source.Stop();
                source.clip = clip;
                source.Play();
                return source;
            }

            Debug.LogWarning($"sfx '{sfxName}' does not exit");
            return null;
        }
    }
}