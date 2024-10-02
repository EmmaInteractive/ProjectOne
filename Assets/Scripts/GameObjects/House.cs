using UnityEngine;
using Assets.Scripts.Services;

namespace Assets.Scripts.GameObjects
{
    public class House : BaseObject, IInteractable
    {
        public string PopupText { get; set; } = "";
        public bool IsEnabled { get; set; } = true;

        [SerializeField]
        private GameObject houseInterior;

        private bool isInteriorActive = false;

        public void Interact()
        {
            if (IsEnabled)
            {
                EnterHouse();
            }
        }

        public bool CanInteract()
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                return distanceToPlayer < 2.0f;
            }

            return false;
        }

        private void EnterHouse()
        {
            if (!isInteriorActive)
            {
                isInteriorActive = true;
                houseInterior.SetActive(true); 

                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
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

        void Update()
        {
            if (CanInteract() && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }
}