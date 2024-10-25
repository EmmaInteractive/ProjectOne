using Assets.Scripts.GameObjects;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class ObjectService : IObjectService, IBaseService
    {
        private readonly List<BaseObject> gameObjects;

        public static ObjectService Instance { get; private set; }

        public ObjectService()
        {
            Instance = this;
            gameObjects = new List<BaseObject>();
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

            // Load in the constructor (instead of Init()) as we cannot ensure that
            // the object service is loaded before the other services.
            GetAllGameObjects();
        }

        private void SceneManager_activeSceneChanged(Scene oldScene, Scene newScene)
        {
            Debug.Log("Scene changed");
            GetAllGameObjects();
        }

        private void GetAllGameObjects()
        {
            Debug.Log("Loading all gameobjects");
            gameObjects.Clear(); 
            var allObjects = GameObject.FindObjectsByType<BaseObject>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            gameObjects.AddRange(allObjects); 
        }

        /// <summary>
        /// Contains all GameObjects of the active Scene
        /// </summary>
        public List<BaseObject> GameObjects => gameObjects;

        public GameObject FindChildByName(GameObject parent, string name)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                var child = parent.transform.GetChild(i);
                if (child.name.Equals(name))
                    return child.gameObject;

                if (child.transform.childCount > 0) 
                {
                    var innerChild = FindChildByName(child.gameObject, name);
                    if (innerChild is not null)
                        return innerChild;
                }
            }
            return null;
        }
           
        public BaseObject FindObjectByName(string name) => 
            FindObjectsByName(name).FirstOrDefault();
        public List<BaseObject> FindObjectsByName(string name) => 
            GameObjects.Where(r => r.name.Equals(name)).ToList();
        
        public BaseObject FindObjectByType<T>(T type) => 
            FindObjectsByType(type).FirstOrDefault();
        public List<BaseObject> FindObjectsByType<T>(T type) => 
            GameObjects.Where(r => r.GetType().IsAssignableFrom(type.GetType())).ToList();
    }
}