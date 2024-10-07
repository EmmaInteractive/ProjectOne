using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private Button newGameButton;
        [SerializeField]
        private Button loadGameButton;
        [SerializeField]
        private Button statsButton;
        [SerializeField]
        private Button optionsButton;
        [SerializeField]
        private Button creditsButton;
        [SerializeField]
        private Button exitButton;
        [SerializeField]
        private Button discordButton;

        [SerializeField]
        private GameObject creditsWindow;

        private ISaveSystemService saveSystemService;

        void Start()
        {
            saveSystemService = new JsonSaveSystemService(new PlayerDataService());

            newGameButton.onClick.AddListener(NewGameButtonAction);
            loadGameButton.onClick.AddListener(LoadGameButtonAction);
            statsButton.onClick.AddListener(StatsButtonAction);
            optionsButton.onClick.AddListener(OptionsButtonAction);
            creditsButton.onClick.AddListener(CreditsButtonAction);
            exitButton.onClick.AddListener(ExitButtonAction);
            discordButton.onClick.AddListener(DiscordButtonAction);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SaveGame();
            }
        }

        private void NewGameButtonAction()
        {
            PlayerData newGameData = new PlayerData();
            saveSystemService.SaveGame(newGameData);

            SceneManager.LoadScene("Town");
        }

        private void LoadGameButtonAction()
        {
            PlayerData loadedData = saveSystemService.LoadGame();
            if (loadedData != null)
            {
                Debug.Log("Save loaded succesfully!");
            }
            else
            {
                Debug.LogError("Save couldnt be loaded.");
            }
        }

        private void SaveGame()
        {
            PlayerData currentData = new PlayerData();
            saveSystemService.SaveGame(currentData);
            Debug.Log("Game saved.");
        }

        private void StatsButtonAction()
        {
            throw new NotImplementedException();
        }

        private void OptionsButtonAction()
        {
            throw new NotImplementedException();
        }

        private void CreditsButtonAction()
        {
            //var title = creditsWindow.transform.Find("Title").gameObject;
            //var text = creditsWindow.transform.Find("Text").gameObject;
            if (creditsWindow.activeInHierarchy)
                return;
            var closeButton = creditsWindow.transform.Find("CloseButton").gameObject;
            closeButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                creditsWindow.SetActive(false);
            });
            creditsWindow.SetActive(true);
        }

        private void ExitButtonAction()
        {
            Application.Quit();
        }

        private void DiscordButtonAction()
        {
            Application.OpenURL("https://discord.gg/4qs2F7Uw");
        }
    }
}