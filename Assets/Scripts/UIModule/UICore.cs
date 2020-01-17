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
                AItem.SetUI(i, new AudioListItemNeed
                {
                    AudioPath = ConstManager.AudioNameList[i],
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
                PlayAudioManager.GetInstance.ContinueAudio(()=> {
                    GetAudio.GetInstance.Get(0, (audio) =>
                    {
                        PlayAudioManager.GetInstance.PlayAudio(audio);
                    });
                });
            }
            else
            {
                IsPause = true;
                Pause_Text.text = ">";
                PlayAudioManager.GetInstance.PauseAudio();
            }
        }
    }
}
