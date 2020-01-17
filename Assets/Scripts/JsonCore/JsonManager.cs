using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace JsonCore
{
    public class JsonManager : MonoBehaviour
    {
        private string JsonPath;
        
        private SaveDataClass _SaveData = new SaveDataClass
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AudioPath = Application.temporaryCachePath + "/../Audio",
#elif UNITY_IOS && !UNITY_EDITOR
#else
            AudioPath = @"D:\AudioOGG",
#endif
        };
        void Awake()
        {
            #region 路径
#if UNITY_ANDROID && !UNITY_EDITOR
        JsonPath = Application.temporaryCachePath + "/../JsonFile/AudioMsg.json";
#elif UNITY_IOS && !UNITY_EDITOR
        JsonPath = Application.temporaryCachePath + "/../JsonFile/AudioMsg.json";
#else
            JsonPath = Application.dataPath + "/../JsonFile/AudioMsg.json";
#endif
            #endregion 

            SaveDataClass saveData = Tools.ToolClass.LoadJsonFile<SaveDataClass>(
                JsonPath,
                () => {
                    Tools.ToolClass.CreateJsonFile(JsonPath, _SaveData);
                }
                );
            AudioManager.ConstManager.AudioDirPath = saveData.AudioPath;
            Debug.Log(saveData.AudioPath);
            LoadFileList();
        }
        /// <summary>
        /// 读取路径下的所有ogg文件
        /// </summary>
        private void LoadFileList()
        {
            if (!Directory.Exists(AudioManager.ConstManager.AudioDirPath))
            {
                return;
            }
            DirectoryInfo audioDir = new DirectoryInfo(AudioManager.ConstManager.AudioDirPath);
            FileInfo[] audios = audioDir.GetFiles();
            for (int i = 0; i < audios.Length; i++)
            {
                if (audios[i].Extension == ".ogg")
                {
                    AudioManager.ConstManager.AudioNameList.Add(audios[i].FullName);
                    //Debug.Log(audios[i].Extension);
                }
            }
        }
    }
}