using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Assets.Shared.Scripts;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;

    private static LoadGamesScenes _loadGamesScenes;

    [SerializeField] GameObject pauseMenuUI;   

    [Header("Buttons")]
    [SerializeField] Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(_loadGamesScenes.loadScene);
    }

    private void Update()
    {
        
        float options = Input.GetAxis("Options");

        if (options > 0 || Input.GetKey(KeyCode.Space) == true)
        {
            if (!GameIsPaused == true)
            {
                Pause();                
            }
            else
            {
                Resume();
            }
        }
    }

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        GameIsPaused = false; 
        
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
