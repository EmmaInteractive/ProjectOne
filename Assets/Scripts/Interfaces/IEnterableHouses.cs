namespace Assets.Scripts.GameObjects
{
    public interface IEnterable
    {
        bool IsEnabled { get; set; }  
        void Enter();                 
        bool CanInteract();           
    }
}