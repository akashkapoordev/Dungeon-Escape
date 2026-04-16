using UnityEditor;
using UnityEngine;
using System.IO;

public class PixelArtImportFixer : EditorWindow
{
    [MenuItem("Tools/Fix Pixel Art Import Settings")]
    static void FixAllTilesetImports()
    {
        string folderPath = "Assets/3rd Party Assets/0x72_16x16DungeonTileset.v5";
        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { folderPath });
        
        int count = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            
            if (importer != null)
            {
                bool changed = false;
                
                if (importer.spritePixelsPerUnit != 16)
                {
                    importer.spritePixelsPerUnit = 16;
                    changed = true;
                }
                
                if (importer.filterMode != FilterMode.Point)
                {
                    importer.filterMode = FilterMode.Point;
                    changed = true;
                }
                
                if (importer.textureCompression != TextureImporterCompression.Uncompressed)
                {
                    importer.textureCompression = TextureImporterCompression.Uncompressed;
                    changed = true;
                }
                
                if (importer.textureType != TextureImporterType.Sprite)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    changed = true;
                }
                
                if (importer.spriteImportMode != SpriteImportMode.Single)
                {
                    importer.spriteImportMode = SpriteImportMode.Single;
                    changed = true;
                }
                
                if (changed)
                {
                    importer.SaveAndReimport();
                    count++;
                }
            }
        }
        
        Debug.Log($"[PixelArtImportFixer] Fixed import settings on {count} textures out of {guids.Length} total in tileset folder.");
        EditorUtility.DisplayDialog("Pixel Art Import Fixer", 
            $"Done! Fixed {count} textures out of {guids.Length} total.\n\nSettings applied:\n- Pixels Per Unit: 16\n- Filter Mode: Point (no filter)\n- Compression: Uncompressed\n- Sprite Mode: Single", 
            "OK");
    }
}
