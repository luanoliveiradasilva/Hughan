using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Assets.MainMenu.Scripts
{
    public class LoadGame : MonoBehaviour
    {

        [SerializeField] private string SceneName;   
        
        [SerializeField] GameObject loadingScreen;
        [SerializeField] GameObject closeCurrentScreen;
        [SerializeField] GameObject OpenNextScreen;

        [SerializeField] private TMP_Text loadingText;

        [SerializeField] Button startButton;
        [SerializeField] Button settingsButton;
        [SerializeField] Button quitButton;
        

        private float loadingTime = 5f;
        private int numDots = 0;

        private void Start()
        {
            startButton.onClick.AddListener(loadScene);            
            settingsButton.onClick.AddListener(SettingsOfGame);
            quitButton.onClick.AddListener(SettingsOfGame);
            loadingScreen.SetActive(false);
        }

        public void loadScene()
        {
            closeCurrentScreen.gameObject.SetActive(false);
            loadingScreen.SetActive(true);
            InvokeRepeating("updateLoadingText", 0f, 0.5f); // Chama o método updateLoadingText a cada 0.5 segundos
            Invoke("loadSceneGame", loadingTime);
        }

        //Carregar a cena em segundo plano de forma assincrona permitindo com que a tela seja completamente carregada.
        private void loadSceneGame()
        {
            SceneManager.LoadSceneAsync(SceneName);
        }

        //Carrega os 3 pontos no texto do loading, para ter uma animacao com o usuário
        private void updateLoadingText()
        {
            numDots++;
            if (numDots > 3) numDots = 1;
            loadingText.text = "Loading" + new string('.', numDots);
        }

        public void SettingsOfGame()
        {
            closeCurrentScreen.gameObject.SetActive(false);
            OpenNextScreen.SetActive(true);
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
