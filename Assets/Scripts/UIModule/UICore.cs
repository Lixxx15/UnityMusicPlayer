using AudioManager;
using Interface.UIInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIModule
{
    public class UICore : MonoBehaviour
    {
        public Transform ListParent;
        public AudioListItem ListItem;
        public PlayAudioManager AudioPlayer;
        public GetAudio _GetAudio;
        public Button Btn_Pause;
        public Text Pause_Text;
        private bool IsPause = true;
        private List<IUIItemSet<AudioListItemNeed>> AudioItemList = new List<IUIItemSet<AudioListItemNeed>>();

        private void Start()
        {
            if (ConstManager.AudioNameList.Count == 0)
            {
                Debug.Log("没有音频文件");
                return;
            }
            LoadItem();
            Btn_Pause.onClick.AddListener(BtnPauseClick);
        }

        private void LoadItem()
        {
            int num = ConstManager.AudioNameList.Count;
            for (int i = 0; i < num; i++)
            {
                GameObject item = Instantiate(ListItem.gameObject);
                item.transform.SetParent(ListParent);
                item.transform.localScale = Vector3.one;
                AudioListItem AItem = item.GetComponent<AudioListItem>();
                AItem.SetUI(new AudioListItemNeed
                {
                    AudioPath = ConstManager.AudioNameList[i],
                    AudioPlayer = this.AudioPlayer,
                    _GetAudio = this._GetAudio,
                    ItemClick = () => {
                        IsPause = false;
                        Pause_Text.text = "||";
                    }
                });
            }
        }

        private void BtnPauseClick()
        {
            if (IsPause)
            {
                IsPause = false;
                Pause_Text.text = "||";
                AudioPlayer.ContinueAudio(()=> {
                    _GetAudio.getAudio(ConstManager.AudioNameList[0], (audio) =>
                    {
                        AudioPlayer.PlayAudio(audio);
                    });
                });
            }
            else
            {
                IsPause = true;
                Pause_Text.text = ">";
                AudioPlayer.PauseAudio();
            }
        }
    }
}
