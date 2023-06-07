using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenuUI;

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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
