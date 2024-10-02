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

        private HouseService houseService; 
        private bool isInteriorActive = false;

        
        public House()
        {
            houseService = new HouseService(); 
        }

        
        public void Interact()
        {
            if (IsEnabled)
            {
                Enter(); 
            }
        }

        
        public bool CanInteract()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return houseService.CanPlayerInteract(gameObject, player, 2.0f); 
        }

        private void Enter() 
        {
            if (!isInteriorActive)
            {
                isInteriorActive = true;
                houseInterior.SetActive(true); 

                GameObject player = GameObject.FindWithTag("Player");
                houseService.TeleportPlayer(player, houseInterior); 
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