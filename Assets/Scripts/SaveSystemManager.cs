using System.Collections.Generic;
using UnityEngine;

public class SaveSystemManager : MonoBehaviour
{
    public static SaveSystemManager Instance { get; private set; }

    private ISaveSystemService saveSystemService;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveSystemService = new JsonSaveSystemService(new PlayerDataService());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerData currentData = new PlayerData();
            SaveGame(currentData, 1);  
        }
    }

   
    public void SaveGame(PlayerData data, int slot)
    {
        saveSystemService.SaveGame(data, slot);
        Debug.Log("Game saved in slot: " + slot);
    }

    
    public PlayerData LoadGame(int slot)
    {
        PlayerData loadedData = saveSystemService.LoadGame(slot);
        if (loadedData != null)
        {
            Debug.Log("Game loaded from slot: " + slot);
        }
        return loadedData;
    }

    
    public List<int> GetAvailableSaveSlots()
    {
        List<int> availableSlots = new List<int>();

        for (int i = 1; i <= 3; i++)  
        {
            if (saveSystemService.IsSaveSlotAvailable(i))
            {
                availableSlots.Add(i);  
            }
        }

        return availableSlots;
    }
}