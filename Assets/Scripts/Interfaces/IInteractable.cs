public interface IInteractable
{
    string PopupText { get; set; }

    bool IsEnabled { get; set; }

    void Interact();
    bool CanInteract();
}
