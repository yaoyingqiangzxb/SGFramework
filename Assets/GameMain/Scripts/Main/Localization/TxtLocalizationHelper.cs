//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Localization;
using System;
using System.IO;
using System.Xml;
using UnityGameFramework.Runtime;

namespace Game
{
    /// <summary>
    /// Txt 格式的本地化辅助器。
    /// </summary>
    public class TxtLocalizationHelper : DefaultLocalizationHelper
    {
        /// <summary>
        /// 解析字典。
        /// </summary>
        /// <param name="dictionaryString">要解析的字典字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析字典成功。</returns>
        public override bool ParseData(ILocalizationManager localizationManager, string dictionaryString, object userData)
        {
            try
            {
                string currentLanguage = GameEntry.Localization.ToString();

                var strLoacalion = File.ReadAllText(dictionaryString);

                var strRows = strLoacalion.Split('\n');

                foreach (var row in strRows) 
                {
                   var keyValues= row.Split('=');

                    string key = keyValues[0];
                    string value = keyValues[1];


                    if (!localizationManager.AddRawString(key, value))
                    {
                        Log.Warning("Can not add raw string with key '{0}' which may be invalid or duplicate.", key);
                        return false;
                    }
                }   
               return true;
            }
            catch (Exception exception)
            {
                Log.Warning("Can not parse dictionary data with exception '{0}'.", exception.ToString());
                return false;
            }
        }
    }
}
