using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AudioSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _source;
        
        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        public AudioSource GetSource()
        {
            gameObject.SetActive(true);
            return _source;
        }

        public void ReturnPlayer()
        {
            gameObject.SetActive(false);
            AudioManager.ReturnAudioPlayer(this);
        }

        private void Update()
        {
            if(!_source.isPlaying)
            {
                ReturnPlayer();
            }
        }
    }
}