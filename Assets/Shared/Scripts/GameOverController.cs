using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private string cenarioName;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject _gameOver;

    private AsyncOperation reloadScene;

    public void StartRestartCoroutine()
    {
      
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {

        loadingScreen.SetActive(true);

        reloadScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        reloadScene.allowSceneActivation = false;

        while (!reloadScene.isDone)
        {
            if (reloadScene.progress >= 0.9f)
            {
                reloadScene.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
