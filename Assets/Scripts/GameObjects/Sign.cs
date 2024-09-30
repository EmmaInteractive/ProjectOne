using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Sign : BaseObject, IInteractable
    {
        [SerializeField]
        private bool isEnabled = true;
        [SerializeField]
        private string _popupText;


        private int _gameTextWindowId;

        public string PopupText { get => _popupText; set => _popupText = value; }
        public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

        public bool CanInteract() => true;

        public void Interact()
        {
            if (!IsEnabled)
                return;

            if (!string.IsNullOrEmpty(PopupText))
                ShowGameText();
        }

        private void ShowGameText()
        {
            _gameTextWindowId = DialogService.Instance.ShowGameTextWindow(transform.position, PopupText);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            DialogService.Instance.CloseGameTextWindow(_gameTextWindowId);
        }
    }
}
