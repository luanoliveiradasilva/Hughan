using UnityEngine;

public class PauseGame : MonoBehaviour
{

    //verificar o pause do jogo
    private static bool _gameIsPaused = false;
    private bool _pauseButtonPressed = false;

    [Header("Painel de Pause")]
    [Tooltip("Carrega o painel de pauso do jogo.")]
    [SerializeField] GameObject pauseMenuUI;

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
