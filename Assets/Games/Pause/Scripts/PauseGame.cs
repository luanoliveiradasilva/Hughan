using UnityEngine;
using UnityEngine.UI;
using Assets.Shared.Scripts;

public class PauseGame : MonoBehaviour
{

    //verificar o pause do jogo
    private static bool _gameIsPaused = false;
    private bool _pauseButtonPressed = false;

    //verificar a cena a ser carregada
    private static LoadGamesScenes _loadGamesScenes;

    [Header("Painel de Pause")]
    [Tooltip("Carrega o painel de pauso do jogo.")]
    [SerializeField] GameObject pauseMenuUI;

    [Header("Buttons")]
    [Tooltip("Carrega o botao de quit do jogo")]
    [SerializeField] Button quitButton;


    private void Start()
    {
        quitButton.onClick.AddListener(_loadGamesScenes.loadScene);
    }

    private void Update()
    {

        if (Input.GetButtonDown("Options") || Input.GetKey(KeyCode.Escape))
        {
            if (!_pauseButtonPressed)
            {
                _pauseButtonPressed = true;
                OnclickPause();
            }
        }
        else
        {
            _pauseButtonPressed = false;
        }
    }

    private void OnclickPause()
    {
        pauseMenuUI.SetActive(_gameIsPaused);
        Time.timeScale = _gameIsPaused ? 0f : 1f;
        _gameIsPaused = !_gameIsPaused;
    }
}
