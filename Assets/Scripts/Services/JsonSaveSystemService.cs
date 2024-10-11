using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class JsonSaveSystemService : ISaveSystemService
{
    private string savePath;

    public void SaveGame(PlayerData data, int slot)
    {
        savePath = Application.persistentDataPath + "/savefile" + slot + ".json";
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved at: " + savePath);
    }

    public PlayerData LoadGame(int slot)
    {
        savePath = Application.persistentDataPath + "/savefile" + slot + ".json";
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Game loaded from slot: " + slot);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in slot: " + slot);
            return null;
        }
    }

    public List<int> GetAvailableSaveSlots()
    {
        List<int> availableSlots = new List<int>();

        for (int i = 1; i <= 3; i++)
        {
            string path = Application.persistentDataPath + "/savefile" + i + ".json";
            if (File.Exists(path))
            {
                availableSlots.Add(i);
            }
        }

        return availableSlots;
    }
 
    public bool IsSaveSlotAvailable(int slot)
    {
        string path = Application.persistentDataPath + "/savefile" + slot + ".json";
        return File.Exists(path);
    }
}