using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D;


namespace Game.Editor
{
    public class SpriteAtlasBuild
    {
        [MenuItem("Assets/UDFramework/SpriteAtlas/Build SpriteAtlas")]
        static void BuildSpriteAtlas()
        {
            string folderPath = EditorUtils.GetSelectedDirAssetsPath();
            string fullPath = EditorUtils.AssetsPath2ABSPath(folderPath);
            BuildSpriteAtlasAtFolderPath(fullPath);
        }

        /// <summary>
        /// 按文件夹创建图集
        /// </summary>
        /// <param name="folderPath">全路径</param>
        static void BuildSpriteAtlasAtFolderPath(string folderPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);
            DirectoryInfo[] subFolders = dInfo.GetDirectories();
         
            if (subFolders == null || subFolders.Length == 0)
            {
                if (folderPath.Contains("Atlas"))
                    BuildSpriteAtlas(folderPath);
            }
            else
            {
                for (int i = 0; i < subFolders.Length; ++i)
                {
                    BuildSpriteAtlasAtFolderPath(subFolders[i].FullName);
                }
            }
        }


        static void BuildSpriteAtlas(string folderPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);

            folderPath = PathHelper.PathFixSplash(folderPath);

            string atlasName = "";
            //ui panel下的图集
            if (dInfo.Name == "Atlas")
            {
                atlasName = dInfo.Parent.Name + "_Atlas";
            }
            else
            {
                atlasName = dInfo.Name+"_Atlas";
            }
            
            string spriteDataPath = folderPath + "/" + atlasName + ".spriteatlas";
            
            if (File.Exists(spriteDataPath))
                File.Delete(spriteDataPath);
            
            FileInfo[] files = dInfo.GetFiles();
            if (files.Length <= 0)
                return;

            List<UnityEngine.Object> textures = new List<UnityEngine.Object>();
            for (int i = 0; i < files.Length; i++)
            {
                string extension = files[i].Extension;
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    string filePath = PathHelper.PathFixSplash(files[i].FullName);
                    string fileAssetsPath = PathHelper.GetAssetsRelatedPath(filePath);
                    Object texture = AssetDatabase.LoadAssetAtPath<Object>(fileAssetsPath);
                    if (texture != null)
                        textures.Add(texture);
                }
            }

            if (textures.Count <= 0)
                return;
            
            Debug.Log("build sprite atlas : "+spriteDataPath);
            
            //create atlas
            SpriteAtlas atlas = new SpriteAtlas();
            SpriteAtlasPackingSettings packSet = new SpriteAtlasPackingSettings()
            {
                enableRotation = false,
                enableTightPacking = false,
                padding = 8,
            };
            atlas.SetPackingSettings(packSet);

            TextureImporterPlatformSettings platform = new TextureImporterPlatformSettings()
            {
                maxTextureSize = 4096,
                format = TextureImporterFormat.Automatic,
                compressionQuality = 50,
                crunchedCompression = true,
                textureCompression = TextureImporterCompression.Compressed,
            };
            atlas.SetPlatformSettings(platform);
            string assetFolderPath = PathHelper.GetAssetsRelatedPath(folderPath);
            string path = assetFolderPath +"/" + atlasName + ".spriteatlas";
            AssetDatabase.CreateAsset(atlas, path);
            
            SpriteAtlasExtensions.Add(atlas, textures.ToArray());
            AssetDatabase.SaveAssets();
        }
    }
}