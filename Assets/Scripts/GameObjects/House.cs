using UnityEngine;
using Assets.Scripts.Services;

namespace Assets.Scripts.GameObjects
{
    public class House : BaseObject, IEnterable
    {
        public bool IsEnabled { get; set; } = true; 
        [SerializeField]
        private GameObject houseInterior; 

        private HouseService houseService; 
        private bool isInteriorActive = false;

        
        public House()
        {
            houseService = new HouseService(); 
        }

       
        public void Enter()
        {
            if (!isInteriorActive && IsEnabled)
            {
                isInteriorActive = true;
                houseInterior.SetActive(true); 

                GameObject player = GameObject.FindWithTag("Player");
                houseService.TeleportPlayer(player, houseInterior); 
            }
        }

        
        public bool CanInteract()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return houseService.CanPlayerInteract(gameObject, player, 2.0f); 
        }

        void Update()
        {
            if (CanInteract() && Input.GetKeyDown(KeyCode.E))
            {
                Enter(); 
            }
        }
    }
}