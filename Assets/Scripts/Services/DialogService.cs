using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class DialogService : IDialogService, IBaseService
    {
        private const string SignTextPrefabPath = "Prefabs/SignTextPrefab";

        public static DialogService Instance { get; private set; }

        public enum DialogResponse
        {
            NONE,
            OK,
            CANCEL,
            ABORT
        }

        private readonly Dictionary<int, GameObject> _gameTextWindows = new();
        private object _gameTextWindowsLock = new();
        private int _lastGameTextWindowId = 0;

        public DialogService()
        {
            Instance = this;
        }

        /// <inheritdoc/>
        public int ShowGameTextWindow(GameObject parent, Vector3 position, string text, int? duration = null, int id = 0)
        {
            if (id != 0)
            {
                _gameTextWindows[id].SetActive(true);
                return id;
            }

            lock (_gameTextWindowsLock)
            {
                id = _lastGameTextWindowId++;
            }

            var sign = Resources.Load<GameObject>(SignTextPrefabPath);

            if (sign != null)
            {
                var signObject = GameObject.Instantiate(sign, new Vector3(0, 1, 0), Quaternion.identity);
                var textObject = signObject.GetComponentInChildren<TextMeshProUGUI>();

                if (textObject is null)
                    return -1;
                textObject.text = text;

                signObject.transform.SetParent(parent.transform, false);
                signObject.SetActive(true);
                _gameTextWindows.Add(id, signObject);
            }

            // If the duration is set start a task that waits for the duration and then closes the window
            if (duration is not null)
            {
                Task.Run(() =>
                {
                    Task.Delay(duration ?? 0);
                    CloseGameTextWindow(id);
                });
            }
            return id;
        }

        /// <inheritdoc/>
        public void CloseGameTextWindow(int id)
        {
            var gameTextWindow = _gameTextWindows.FirstOrDefault(r => r.Key == id);
            if (gameTextWindow.Value is null)
                return;

            gameTextWindow.Value.SetActive(false);
        }
    }
}
