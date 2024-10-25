using static Assets.Scripts.Services.ClassSelectorService;

namespace Assets.Scripts.Interfaces
{
    public enum ClassType
    {
        Damage,
        Tank,
        Support
    }

    internal interface IGameClass
    {
        public string Name { get; }
        public string Description { get; }
        public ClassType ClassType { get; }
        public GameClass GameClass { get; }
        public int STR { get; set; }
        public int INT { get; set; }
        public int DEX { get; set; }
        public string PreviewSpriteResource { get; set; }
        public string PreviewSpriteResourceIndex { get; set; }
    }
}
