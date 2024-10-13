using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Scripts.Services
{
    internal class ClassSelectorService : IBaseService
    {
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

        public ClassSelectorService()
        {
            Instance = this;
            GetGameClasses();

            SelectClass(GameClass.ShadowDancer);
        }

        public void SelectClass(GameClass gameClass)
        {
            var charPreview = GameObject.Find("CharacterPreview");
            var gameClassObject = _gameClasses.FirstOrDefault(r => r.GameClass == gameClass);
            var className = gameClassObject.Name;
            charPreview.GetComponent<Animator>().Play($"ClassPreview_{className}");
            SelectedClass = gameClassObject;
            UpdateUI();
        }

        private void UpdateUI()
        {
            var strText = GameObject.Find("StrValue");
            var intText = GameObject.Find("IntValue");
            var decText = GameObject.Find("DexValue");
            var classInfoText = GameObject.Find("ClassInfoText");
            var classNameText = GameObject.Find("PreviewClassName");

            strText.GetComponent<TextMeshProUGUI>().text = SelectedClass.STR.ToString();
            intText.GetComponent<TextMeshProUGUI>().text = SelectedClass.INT.ToString();
            decText.GetComponent<TextMeshProUGUI>().text = SelectedClass.DEX.ToString();
            classInfoText.GetComponent<TextMeshProUGUI>().text = SelectedClass.Description;
            classNameText.GetComponent<TextMeshProUGUI>().text = SelectedClass.Name;
        }

        private void GetGameClasses()
        {
            var list = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IGameClass).IsAssignableFrom(t) && !t.IsInterface)
                .Select(t => t)
                .ToList();

            foreach (var item in list)
            {
                _gameClasses.Add((IGameClass)Activator.CreateInstance(item));
            }
        }
    }
}
