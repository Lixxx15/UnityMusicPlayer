using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManager
{
    public class PlayAudioManager : Manager.MonoSingleton<PlayAudioManager>
    {
        /// <summary>
        /// 当前正在播放的Audio
        /// </summary>
        private AudioSource _AudioSource;

        private int _NowAudioIndex;
        /// <summary>
        /// 设置当前播放的音乐的下标
        /// </summary>
        /// <param name="index"></param>
        public void SetNowAudioIndex(int index)
        {
            if (index >= ConstManager.AudioNameList.Count)
            {
                index = 0;
            }
            else if(index < 0)
            {
                index = ConstManager.AudioNameList.Count - 1;
            }
            _NowAudioIndex = index;
        }

        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="newClip"></param>
        public void PlayAudio(AudioClip newClip)
        {
            _AudioSource.clip = newClip;
            _AudioSource.Play();
        }
        /// <summary>
        /// 下一首
        /// </summary>
        public void NextAudio()
        {
            SetNowAudioIndex(_NowAudioIndex + 1);
            GetAudio.GetInstance.Get(_NowAudioIndex, PlayAudio);
        }
        /// <summary>
        /// 上一首
        /// </summary>
        public void BeforeAudio()
        {
            SetNowAudioIndex(_NowAudioIndex - 1);
            GetAudio.GetInstance.Get(_NowAudioIndex, PlayAudio);
        }
        /// <summary>
        /// 暂停
        /// </summary>
        public void PauseAudio()
        {
            _AudioSource.Pause();
        }
        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="newClip">当前没有正在播放的Audio时执行</param>
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
