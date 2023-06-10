using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [Header("Sound")]
    [Tooltip("Selecionar o audio")]
    [SerializeField] private AudioClip soundRunner;
    [SerializeField] private AudioClip soundAttack;
    [SerializeField] private AudioClip soundDeath;
    [SerializeField] private AudioSource audioSource;

    public void Attack()
    {
        audioSource.clip = soundAttack;
        audioSource.Play();
    }

    public void Runner()
    {
        audioSource.clip = soundRunner;
        audioSource.Play();
    }

    public void Death()
    {
        audioSource.clip = soundDeath;
        audioSource.Play();
    }
}
