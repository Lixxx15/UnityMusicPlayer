using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManager;
using System.IO;

namespace UIModule
{
    [RequireComponent(typeof(Button))]
    public class AudioListItem : MonoBehaviour, Interface.UIInterface.IUIItemSet<AudioListItemNeed>
    {
        public Text AudioName;
        private Button Btn_Click;
        private FileInfo AudioFile;
        private AudioListItemNeed _Need;
        private int num;

        public void SetUI(int index, AudioListItemNeed t)
        {
            num = index;
            if (!File.Exists(t.AudioPath))
            {
                Debug.LogError(t.AudioPath + "#不存在文件");
                return;
            }
            _Need = t;
            AudioFile = new FileInfo(t.AudioPath);
            AudioName.text = AudioFile.Name;
            Btn_Click = GetComponent<Button>();
            Btn_Click.onClick.AddListener(PlayAudio);
        }

        public void PlayAudio()
        {
            GetAudio.GetInstance.Get(num, PlayAudioManager.GetInstance.PlayAudio);
            _Need.ItemClick();
        }
    }
}
