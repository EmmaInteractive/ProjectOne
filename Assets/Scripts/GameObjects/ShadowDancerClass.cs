using Assets.Scripts.Interfaces;
using UnityEngine;
using static Assets.Scripts.Services.ClassSelectorService;

namespace Assets.Scripts.GameObjects
{
    internal class ShadowDancerClass : ScriptableObject, IGameClass
    {
        /// <summary>
        /// Name has to equal the animation name.
        /// </summary>
        public string Name => "ShadowDancer";
        public string Description => "He's epic!";
        public ClassType ClassType => ClassType.Damage;
        public GameClass GameClass => GameClass.ShadowDancer;
        public int STR { get; set; } = 42;
        public int INT { get; set; } = 42;
        public int DEX { get; set; } = 42;
        public string PreviewSpriteResource { get; set; } = "Sprites/NPCS/ShadowDancer";
        public string PreviewSpriteResourceIndex { get; set; } = "20";
    }
}
