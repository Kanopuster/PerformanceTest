using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class MeshReadWriteUtility
{
    private static List<string> modifiedMeshes = new List<string>();
    private static string savePath = "Assets/modifiedMeshes.txt";

    [MenuItem("Tools/Enable Read/Write for All Meshes")]
    public static void EnableReadWriteForAllMeshes()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer != null && !importer.isReadable)
            {
                importer.isReadable = true;
                AssetDatabase.ImportAsset(path);
                modifiedMeshes.Add(path);
            }
        }
        SaveModifiedMeshes();
        Debug.Log("Enabled Read/Write for all meshes in the project.");
    }

    [MenuItem("Tools/Disable Read/Write for Modified Meshes")]
    public static void DisableReadWriteForModifiedMeshes()
    {
        LoadModifiedMeshes();
        foreach (string path in modifiedMeshes)
        {
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer != null && importer.isReadable)
            {
                importer.isReadable = false;
                AssetDatabase.ImportAsset(path);
            }
        }
        modifiedMeshes.Clear();
        File.Delete(savePath);
        Debug.Log("Disabled Read/Write for modified meshes.");
    }

    private static void SaveModifiedMeshes()
    {
        using (StreamWriter writer = new StreamWriter(savePath))
        {
            foreach (string path in modifiedMeshes)
            {
                writer.WriteLine(path);
            }
        }
    }

    private static void LoadModifiedMeshes()
    {
        if (File.Exists(savePath))
        {
            using (StreamReader reader = new StreamReader(savePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    modifiedMeshes.Add(line);
                }
            }
        }
    }
}
