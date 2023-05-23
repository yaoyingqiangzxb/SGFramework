//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.DataTableTools
{
    

    public sealed class DataTableGeneratorMenu
    {
        private static string m_dataTablePath = Path.Combine(Application.dataPath, "GameMain/DataTables");
        [MenuItem("GameTools/Generate DataTables")]
        private static void GenerateDataTables()
        {

            // 如果文件夹不存在则创建
            if (!Directory.Exists(m_dataTablePath))
            {
                Directory.CreateDirectory(m_dataTablePath);
                return;
            }

            DirectoryInfo directoryInfo =new DirectoryInfo (m_dataTablePath);
            var files= directoryInfo.GetFiles("*.txt");
            foreach (var file in files)
            {
                string dataTableName=file.Name.Split('.')[0];
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
    }
}
