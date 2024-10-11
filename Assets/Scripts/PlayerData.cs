using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData
{
    public readonly string SceneName = "Town";
    public readonly Vector3 Position;

    public PlayerData()
    {
        Position = GameObject.FindWithTag("Player")?.transform.position ?? Vector3.zero;
    }
}