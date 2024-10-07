using System.Collections.Generic;

public class PlayerDataService : IPlayerDataService
{
    private PlayerData _currentPlayerData;

    public PlayerDataService()
    {
        
        _currentPlayerData = new PlayerData();
    }

    
    public PlayerData GetPlayerData()
    {
        return _currentPlayerData;
    }

    
    public void SetPlayerData(PlayerData data)
    {
        _currentPlayerData = data;
    }
}