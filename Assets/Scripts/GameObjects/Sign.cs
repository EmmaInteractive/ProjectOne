using Assets.Scripts.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Sign : BaseObject, IInteractable
    {
        public string PopupText { get; set; }

        [SerializeField]
        public bool IsEnabled { get; set; }

        private Rigidbody2D _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

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
            var ownPosition = _rb.transform.position;
            DialogService.Instance.ShowGameTextWindow(ownPosition, PopupText);
        }
    }
}
