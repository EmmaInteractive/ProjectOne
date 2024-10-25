using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string Description => "Here is the lore of this character.";
        public ClassType ClassType => ClassType.Damage;
        public GameClass GameClass => GameClass.ShadowDancer;
        public int STR { get; set; } = 5;
        public int INT { get; set; } = 2;
        public int DEX { get; set; } = 3;
        public string PreviewSpriteResource { get; set; } = "Sprites/NPCS/ShadowDancer";
    }
}
