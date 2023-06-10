using UnityEngine;

namespace Assets.MainMenu.Scripts
{
    class SoundMouse : MonoBehaviour
    {

        [Header("Sound")]
        [Tooltip("Selecionar o audio")]
        [SerializeField] private AudioClip soundClip;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        private void Start()
        {
            audioSource.volume = 0.3f;
        }

        public void OnMouseEnter()
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
          
            if (collision.CompareTag("Cursor"))
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
        }
    }
}
