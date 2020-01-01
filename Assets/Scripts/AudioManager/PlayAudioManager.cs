using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManager
{
    public class PlayAudioManager : MonoBehaviour
    {
        public AudioSource _AudioSource;
        
        public void PlayAudio(AudioClip newClip)
        {
            _AudioSource.clip = newClip;
            _AudioSource.Play();
        }
        public void PauseAudio()
        {
            _AudioSource.Pause();
        }
        public void ContinueAudio(Action newClip)
        {
            if (_AudioSource.clip == null)
            {
                newClip();
                return;
            }
            _AudioSource.Play();
        }
    }
}
