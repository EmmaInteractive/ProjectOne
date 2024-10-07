public interface ISaveSystemService
{
    void SaveGame(PlayerData data);
    PlayerData LoadGame();
}