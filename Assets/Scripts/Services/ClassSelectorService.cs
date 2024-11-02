using Assets.Scripts.GameObjects;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Services
{
    internal class ClassSelectorService : IBaseService
    {
        public ObjectService _os;

        private readonly List<IGameClass> _gameClasses = new List<IGameClass>();

        public static ClassSelectorService Instance;

        public enum GameClass
        {
            ShadowDancer,
            Berserker,
            Arcanist,
            Necromancer,
            Runemaster,
            Geomancer,
            Stormbringer,
            Druid,
            Bard,
            Alchemist,
            Summoner,
            Lifeweaver
        }

        public IGameClass SelectedClass { get; private set; }

        private BaseObject ClassSelectorUIPanel { get; set; }
        private BaseObject CharacterPreview { get; set; }

        public bool IsActive
        {
            get => ClassSelectorUIPanel.gameObject.activeInHierarchy;
            set => ClassSelectorUIPanel.gameObject.SetActive(value);
        }

        public ClassSelectorService()
        {
            Instance = this;
        }

        public void Init()
        {
            _os = ObjectService.Instance;
            CharacterPreview = _os.FindObjectByName("CharacterPreview");
            ClassSelectorUIPanel = _os.FindObjectByName("ClassSelector");
        }

        public void ShowUI()
        {
            IsActive = true;
            if (_gameClasses.Count == 0)
            {
                var cancelButton = _os.FindChildByName(ClassSelectorUIPanel.gameObject, "CancelButton");
                var continueButton = _os.FindChildByName(ClassSelectorUIPanel.gameObject, "ContinueButton");

                if (cancelButton is null)
                    throw new NullReferenceException("cancelButton not found");
                if (continueButton is null)
                    throw new NullReferenceException("continueButton not found");

                cancelButton.GetComponent<Button>().onClick.AddListener(CancelButtonAction);
                continueButton.GetComponent<Button>().onClick.AddListener(ContinueButtonAction);

                GetGameClasses();
            }
            SelectClass(GameClass.ShadowDancer);
        }

        private void ContinueButtonAction()
        {
            _os.OnObjectsLoaded += _os_OnObjectsLoaded;

            TeleportationService.Instance.LoadSceneAndTeleportPlayer("Town", new Vector3(4, -4, 0));
            
            IsActive = false;
        }

        private void _os_OnObjectsLoaded(object sender, EventArgs e)
        {
            var player = _os.FindObjectByName("Player");
            if (player is null)
                Debug.Log("Player is null");

            player.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animations/{SelectedClass.Name}/{SelectedClass.Name}.controller", typeof(RuntimeAnimatorController));
        }

        private void CancelButtonAction()
        {
            IsActive = false;
        }

        public void SelectClass(GameClass gameClass)
        {
            var charPreview = CharacterPreview;
            var gameClassObject = _gameClasses.FirstOrDefault(r => r.GameClass == gameClass);
            var className = gameClassObject.Name;
            charPreview.GetComponent<Animator>().Play($"ClassPreview_{className}");
            SelectedClass = gameClassObject;
            UpdateUI();
        }

        private void UpdateUI()
        {
            var statsPanel = _os.FindObjectByName("StatsPanel");
            var strText = _os.FindChildByName(statsPanel.gameObject, "StrValue");
            var intText = _os.FindChildByName(statsPanel.gameObject, "IntValue");
            var decText = _os.FindChildByName(statsPanel.gameObject, "DexValue");
            strText.GetComponent<TextMeshProUGUI>().text = SelectedClass.STR.ToString();
            intText.GetComponent<TextMeshProUGUI>().text = SelectedClass.INT.ToString();
            decText.GetComponent<TextMeshProUGUI>().text = SelectedClass.DEX.ToString();

            var classInfoPanel = _os.FindObjectByName("ClassInfoPanel");
            var classInfoText = _os.FindChildByName(classInfoPanel.gameObject, "ClassInfoText");
            classInfoText.GetComponent<TextMeshProUGUI>().text = SelectedClass.Description;

            var previewPanel = _os.FindObjectByName("PreviewPanel");
            var previewClassName = _os.FindChildByName(previewPanel.gameObject, "PreviewClassName");
            previewClassName.GetComponent<TextMeshProUGUI>().text = SelectedClass.Name;
        }

        private void GetGameClasses()
        {
            var list = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IGameClass).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => t)
                .ToList();

            foreach (var item in list)
            {
                var gameClass = (IGameClass)Activator.CreateInstance(item);
                _gameClasses.Add(gameClass);
                CreatePreviewButton(gameClass);
            }
        }

        private void CreatePreviewButton(IGameClass gameClass)
        {
            GameObject classPanel = null;
            switch(gameClass.ClassType)
            {
                case ClassType.Damage:
                    classPanel = _os.FindChildByName(ClassSelectorUIPanel.gameObject, "DamageClassPanel");
                    break;
                case ClassType.Tank:
                    classPanel = _os.FindChildByName(ClassSelectorUIPanel.gameObject, "TankClassPanel");
                    break;
                case ClassType.Support:
                    classPanel = _os.FindChildByName(ClassSelectorUIPanel.gameObject, "SupportClassPanel");
                    break;
            }
            var classesPanel = _os.FindChildByName(classPanel, "Classes");
            
            for(int i = 0; i < classesPanel.transform.childCount; i++)
            {
                var panel = classesPanel.transform.GetChild(i);
                if (!panel.name.StartsWith("Panel"))
                    continue;

                var btn = new GameObject("Button");
                btn.transform.SetParent(panel);

                var rect = btn.AddComponent<RectTransform>();
                rect.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 80);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 80);

                var image = btn.AddComponent<Image>();
                image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
                image.type = Image.Type.Sliced;
                Color color = Color.white;
                switch (gameClass.ClassType)
                {
                    case ClassType.Damage:
                        UnityEngine.ColorUtility.TryParseHtmlString("#8C0E0EEE", out color);
                        break;
                    case ClassType.Tank:
                        UnityEngine.ColorUtility.TryParseHtmlString("#21328EEE", out color);
                        break;
                    case ClassType.Support:
                        UnityEngine.ColorUtility.TryParseHtmlString("#216A25EE", out color);
                        break;
                }
                image.color = color;

                var button = btn.AddComponent<Button>();
                button.onClick.AddListener(() => 
                {
                    SelectClass(gameClass.GameClass);
                });

                var previewImageObject = new GameObject("PreviewImage");
                var previewImage = previewImageObject.AddComponent<Image>();

                foreach(var resource in Resources.LoadAll<Sprite>(gameClass.PreviewSpriteResource))
                {
                    if (resource.name.Equals($"{gameClass.Name}_{gameClass.PreviewSpriteResourceIndex}"))
                    {
                        previewImage.sprite = (Sprite)resource;
                        break;
                    }
                }
                
                previewImage.transform.SetParent(btn.transform);
                var previewImageRect = previewImageObject.GetComponent<RectTransform>();
                previewImageRect.localScale = new Vector3(2,2,0);
                previewImageRect.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                previewImageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 24);
                previewImageRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 35);

                panel.name = $"{gameClass.Name}Panel";
                break;
            }
        }
    }
}
