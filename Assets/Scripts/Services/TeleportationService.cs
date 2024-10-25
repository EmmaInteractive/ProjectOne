using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class TeleportationService : IBaseService
    {
        public static TeleportationService Instance { get; private set; }

        public TeleportationService()
        {
            Instance = this;
        }

        public void TeleportPlayer(GameObject player, Vector3 destination)
        {
            if (player != null && destination != null)
            {
                destination.z += 0.1f;

                player.transform.position = destination;
                player.transform.rotation = Quaternion.identity;
            }
            SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoaded(destination);
        }

        public void LoadSceneAndTeleportPlayer(string sceneName, Vector3 targetPosition)
        {
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(targetPosition);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void OnSceneLoaded(Vector3 targetPosition)
            => TeleportPlayer(GameObject.FindWithTag("Player"), targetPosition);
    }
}