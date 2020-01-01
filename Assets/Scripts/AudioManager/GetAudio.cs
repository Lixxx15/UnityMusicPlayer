using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace AudioManager
{
    public class GetAudio:MonoBehaviour
    {
        
        public void getAudio(string path, Action<AudioClip> getAudioClip)
        {
            StartCoroutine(LoadAudio(path, getAudioClip));
        }

        private IEnumerator LoadAudio(string path, Action<AudioClip> getAudioClip)
        {
            UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.OGGVORBIS);
            yield return uwr.SendWebRequest();
            if (uwr.isHttpError || uwr.isNetworkError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                AudioClip audio = DownloadHandlerAudioClip.GetContent(uwr);
                getAudioClip(audio);
            }
        }
        
    }
}
