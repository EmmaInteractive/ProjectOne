using System.Collections.Generic;

public interface ISaveSystemService
{
    void SaveGame(PlayerData data, int slot);  
    PlayerData LoadGame(int slot);
    List<int> GetAvailableSaveSlots();
    bool IsSaveSlotAvailable(int slot); 
}