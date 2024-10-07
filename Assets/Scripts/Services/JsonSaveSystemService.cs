using System.IO;
using UnityEngine;

public class JsonSaveSystemService : ISaveSystemService
{
    private string savePath;
    private readonly IPlayerDataService _playerDataService;

    public JsonSaveSystemService(IPlayerDataService playerDataService)
    {
        savePath = Application.persistentDataPath + "/savefile.json";
        _playerDataService = playerDataService;
    }

    
    public void SaveGame(PlayerData data)
    {
        string json = JsonUtility.ToJson(data); 
        File.WriteAllText(savePath, json); 
        Debug.Log("Saved at: " + savePath);
    }

    
    public PlayerData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath); 
            PlayerData data = JsonUtility.FromJson<PlayerData>(json); 
            _playerDataService.SetPlayerData(data); 
            Debug.Log("Save loaded");
            return data; 
        }
        else
        {
            Debug.LogError("Save not found");
            return null; 
        }
    }
}