using UnityEngine;

namespace Assets.Scripts.Services
{
    public class TeleportationService : IBaseService
    {
        public static TeleportationService Instance { get; private set; }

        public TeleportationService()
        {
            Instance = this;
        }

        public void TeleportPlayer(GameObject player, GameObject houseInterior)
        {
            if (player != null && houseInterior != null)
            {
                float xPosition = houseInterior.transform.position.x;
                float yPosition = houseInterior.transform.position.y;
                float zPosition = houseInterior.transform.position.z;

               
                zPosition += 0.1f;

                Vector3 newPosition = new Vector3(xPosition, yPosition, zPosition);
                Debug.Log($"Teleporting player from {player.transform.position} to {newPosition}");
                player.transform.position = newPosition;
                player.transform.rotation = Quaternion.identity;
                Debug.Log($"Player new position: {player.transform.position}");
            }
        }
    }
}