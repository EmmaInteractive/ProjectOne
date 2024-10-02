using UnityEngine;

namespace Assets.Scripts.Services
{
    public class HouseService
    {
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

        public bool CanPlayerInteract(GameObject house, GameObject player, float interactionDistance)
        {
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(house.transform.position, player.transform.position);
                return distanceToPlayer < interactionDistance;
            }

            return false;
        }
    }
}