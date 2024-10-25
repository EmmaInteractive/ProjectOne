using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Services;

namespace Assets.Scripts.GameObjects
{
    public class House : BaseObject, IInteractable
    {
        public string PopupText { get; set; } = "";
        public bool IsEnabled { get; set; } = true;

        [SerializeField]
        private GameObject houseInterior;

        private TeleportationService teleportationService;
        private bool isInteriorActive = false;

        [SerializeField]
        private bool isDungeonEntrance = false;

        [SerializeField]
        private bool isGuildHallEntrance = false;

        public House()
        {
            teleportationService = new TeleportationService();
        }

        public void Interact()
        {
            if (IsEnabled)
            {
                if (isDungeonEntrance)
                {
                    EnterDungeon();
                }
                else if (isGuildHallEntrance)
                {
                    EnterGuildHall();
                }
                else if (houseInterior != null)
                {
                    EnterHouse();
                }
            }
        }

        private void EnterHouse()
        {
            if (!isInteriorActive)
            {
                isInteriorActive = true;
                houseInterior.SetActive(true);
                GameObject player = GameObject.FindWithTag("Player");
                teleportationService.TeleportPlayer(player, houseInterior.transform.position);
            }
        }

        private void EnterDungeon()
        {
            teleportationService.LoadSceneAndTeleportPlayer("DungeonScene", GameObject.Find("DungeonInterior").transform.position);
        }

        private void EnterGuildHall()
        {
            teleportationService.LoadSceneAndTeleportPlayer("GuildHallScene", GameObject.Find("GuildHallInterior").transform.position);
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

        void Update()
        {
            if (CanInteract() && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }
}