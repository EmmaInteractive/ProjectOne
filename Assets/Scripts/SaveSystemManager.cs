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

    public void SaveGame(PlayerData data)
    {
        saveSystemService.SaveGame(data);
        Debug.Log("Game saved.");
    }

    public PlayerData LoadGame()
    {
        return saveSystemService.LoadGame();
    }
}