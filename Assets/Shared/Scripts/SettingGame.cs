using UnityEngine;
using UnityEngine.UI;


public class SettingGame : MonoBehaviour
{


    [Header("Settings")]
    [Tooltip("Close of screen")]
    [SerializeField] GameObject closeCurrentScreen;
    [Tooltip("Open next screen")]
    [SerializeField] GameObject OpenNextScreen;

    [Tooltip("Close menu settings")]
    [SerializeField] GameObject _closeSettingsScreen;
    [Tooltip("Open main menu")]
    [SerializeField] GameObject _openMainMenuScreen;

    [SerializeField] Button _back;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void SettingsOfGame()
    {
        closeCurrentScreen.gameObject.SetActive(false);
        OpenNextScreen.SetActive(true);
    }

    public void BackMenuOfGame()
    {
        _closeSettingsScreen.gameObject.SetActive(false);
        _openMainMenuScreen.SetActive(true);
    }

}
