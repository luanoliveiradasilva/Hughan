using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Assets.Shared.Scripts;

namespace Assets.MainMenu.Scripts
{
    public class LoadGame : MonoBehaviour
    {

        private static LoadGamesScenes _loadGamesScenes;
        private static SettingGame _settingGame; 

        [Header("Buttons")]
        [SerializeField] Button startButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button quitButton;
        

        private void Start()
        {
            startButton.onClick.AddListener(_loadGamesScenes.loadScene);
            settingsButton.onClick.AddListener(_settingGame.SettingsOfGame);
            quitButton.onClick.AddListener(QuitTheGame);
        }

        public void QuitTheGame()
        {
        #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
        #else
                            Application.Quit();
        #endif
        }
    }
}
