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

        private TeleportationService houseService; 
        private bool isInteriorActive = false;

        void Start()
        {
            houseService = TeleportationService.Instance;
        }
        
        public void Interact()
        {
            if (IsEnabled && CanInteract())
            {
                Enter();
            }
        }

        public bool CanInteract() => true;

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
    }
}