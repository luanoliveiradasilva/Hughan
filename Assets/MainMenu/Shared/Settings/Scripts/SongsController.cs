using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SongsController : MonoBehaviour
{
    [Header("Settings song and music")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private Slider _sliderMusicSetting;

    private void Awake()
    {
        _musicSource = GetComponent<AudioSource>();
    }

    public void volumeMusicSettings()
    {
        _musicSource.volume = _sliderMusicSetting.value;
    }
}
