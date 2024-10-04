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

                Debug.Log($"Player teleported to: {destination}");
            }
        }

       
        public void LoadSceneAndTeleport(string sceneName, string targetObjectName)
        {
            
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, targetObjectName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single); 
        }

        private void OnSceneLoaded(Scene scene, string targetObjectName)
        {
            
            GameObject player = GameObject.FindWithTag("Player");
            GameObject targetObject = GameObject.Find(targetObjectName);

            if (player != null && targetObject != null)
            {
                Vector3 targetPosition = targetObject.transform.position;
                player.transform.position = targetPosition;
                player.transform.rotation = Quaternion.identity;

                Debug.Log($"Player teleported to: {targetPosition} in scene: {scene.name}");
            }
            else
            {
                Debug.LogError("Player or targetObject not found in the new scene.");
            }

            
            SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoaded(scene, targetObjectName);
        }
    }
}