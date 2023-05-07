using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Assets.MainMenu.Scripts
{
    public class LoadGame : MonoBehaviour
    {
        
        [SerializeField] private string SceneName;
        [SerializeField] private TMP_Text loadingText;
        private float loadingTime = 5f;
        public Button startButton;
        public GameObject loadingScreen;
        private int numDots = 0;

        private void Start()
        {
            startButton.onClick.AddListener(loadScene);
            loadingScreen.SetActive(false);
        }

        public void loadScene(){

            startButton.gameObject.SetActive(false);
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

    }
}
