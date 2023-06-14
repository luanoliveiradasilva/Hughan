using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{


    [SerializeField] private AudioSource loadingSong;
    [SerializeField] private AudioClip loadingAudioClip;
    // Start is called before the first frame update
    private void Awake()
    {
        loadingSong = GetComponent<AudioSource>();
        loadingAudioClip = GetComponent<AudioClip>();
    }

    private void Start()
    {
        loadingSong.clip = loadingAudioClip;
        loadingSong.Play();
    }
}
