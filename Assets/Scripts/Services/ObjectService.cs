using Assets.Scripts.GameObjects;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class ObjectService : IObjectService
    {
        private readonly List<BaseObject> gameObjects;

        public static ObjectService Instance { get; private set; }

        public ObjectService()
        {
            Instance = this;

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(Scene oldScene, Scene newScene)
        {
            GetAllGameObjects();
        }

        private void GetAllGameObjects()
        {
            GameObjects.Clear();
            var allObjects = GameObject.FindObjectsByType<BaseObject>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            GameObjects.AddRange(allObjects);
        }

        /// <summary>
        /// Contains all GameObjects of the active Scene
        /// </summary>
        public List<BaseObject> GameObjects => gameObjects;

        public BaseObject FindObjectByType<T>(T type) => FindObjectsByType(type).FirstOrDefault();
        public List<BaseObject> FindObjectsByType<T>(T type) => GameObjects.Where(r => r.GetType().IsAssignableFrom(type.GetType())).ToList();
    }
}
