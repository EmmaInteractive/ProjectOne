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
        private const string GameCanvasName = "GameCanvas";
        private const string SignTextPrefabPath = "Prefabs/SignText";

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
        public int ShowGameTextWindow(Vector3 position, string text, int? duration = null)
        {
            int id = 0;

            lock (_gameTextWindowsLock)
            {
                id = _lastGameTextWindowId++;
            }

            // do stuff
            var sign = Resources.Load<GameObject>(SignTextPrefabPath);

            if (sign != null)
            {
                //var worldPosition = position;
                //var screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);

                var signObject = GameObject.Instantiate(sign, screenPosition, Quaternion.identity);
                var textObject = signObject.GetComponentInChildren<TextMeshProUGUI>();

                sign.GetComponent<RectTransform>().transform.position = screenPosition;
                if (textObject is null)
                    return -1;
                textObject.text = text;

                var canvas = GameObject.Find(GameCanvasName);

                signObject.transform.SetParent(canvas.transform, false);
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

            // Do stuff
        }
    }
}
