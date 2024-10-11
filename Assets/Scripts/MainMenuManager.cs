using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    internal class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button statsButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button discordButton;
        [SerializeField] private GameObject creditsWindow;

        private ISaveSystemService saveSystemService;

        void Start()
        {
            saveSystemService = new JsonSaveSystemService();

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
            saveSystemService.SaveGame(newGameData, 1);
            SceneManager.LoadScene("Town");
        }

        private void LoadGameButtonAction()
        {
            // loading screen
            PlayerData loadedData = saveSystemService.LoadGame(1);

            if (loadedData != null)
            {
                SceneManager.LoadScene(loadedData.SceneName);
                // This doesn't need to be a coroutine
                StartCoroutine(SetPlayerPosition(loadedData));
                Debug.Log("Save from Slot 1 loaded successfully!");
            }
            else
            {
                Debug.LogError("Save from Slot 1 couldn't be loaded.");
            }
        }

        private void SaveGame()
        {
            PlayerData currentData = new PlayerData();
            saveSystemService.SaveGame(currentData, 1);
            Debug.Log("Game saved.");
        }

        private IEnumerator SetPlayerPosition(PlayerData loadedData)
        {
            yield return new WaitForSeconds(0.1f);

            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = loadedData.Position;
                Debug.Log("Player position restored.");
            }
            else
            {
                Debug.LogError("Player not found in the scene.");
            }
        }

        private void StatsButtonAction()
        {
            throw new System.NotImplementedException();
        }

        private void OptionsButtonAction()
        {
            throw new System.NotImplementedException();
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