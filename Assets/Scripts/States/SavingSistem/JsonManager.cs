using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    public Vector3 playerPosition;
    public int playerLives;
}

[System.Serializable]
public class WorldData
{
    // Agrega aquí los campos específicos para WorldData
}

[System.Serializable]
public class EnemyData
{
    // Agrega aquí los campos específicos para EnemyData
}

public static class JsonManager
{
    private static string GetSavePath<T>()
    {
        string typeName = typeof(T).Name;
        return Application.persistentDataPath + "/" + typeName + "Data.json";
    }

    public static void SaveData<T>(T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        string savePath = GetSavePath<T>();
        File.WriteAllText(savePath, jsonData);
    }

    public static T LoadData<T>()
    {
        string savePath = GetSavePath<T>();
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            return default(T);
        }
    }

    public static string GetSavePath()
    {
        return Application.persistentDataPath;
    }
}



