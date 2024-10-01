using Assets.Scripts.GameObjects;
using System.Collections.Generic;

namespace Assets.Scripts.Interfaces
{
    public interface IObjectService
    {
        public List<BaseObject> GameObjects { get; }
    }
}