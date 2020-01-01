using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Tools
{
    public class ToolClass
    {
        /// <summary>
        /// 创建json文件
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="jsonObj"></param>
        public static void CreateJsonFile(string jsonPath, object jsonObj)
        {
            FileInfo fileInfo = new FileInfo(jsonPath);
            string jsonData = JsonUtility.ToJson(jsonObj, true);
            if (!Directory.Exists(fileInfo.DirectoryName))
            {
                Directory.CreateDirectory(fileInfo.DirectoryName);
            }
            File.Create(jsonPath).Close();
            File.WriteAllText(jsonPath, jsonData);
        }
        /// <summary>
        /// 读取json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonPath"></param>
        /// <param name="IfExistsntFIle"></param>
        /// <returns></returns>
        public static T LoadJsonFile<T>(string jsonPath,Action IfExistsntFIle = null) where T:class
        {
            if (!File.Exists(jsonPath))
            {
                if (IfExistsntFIle == null)
                {
                    return null;
                }
                IfExistsntFIle();
                return LoadJsonFile<T>(jsonPath);
            }
            string jsonData = File.ReadAllText(jsonPath);
            T jsonObj = JsonUtility.FromJson<T>(jsonData);
            return jsonObj;
        }
    }
}
