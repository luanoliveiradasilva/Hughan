using UnityEngine;

namespace Assets.MainMenu.Scripts
{
    class SoundMouse: MonoBehaviour
    {

        public AudioClip soundClip;
        private AudioSource audioSource;

        public float cursorSpeed = 5f;

        private void Start()
        {
            // Adicione um componente AudioSource ao objeto que possui esse script
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundClip;
        }

        private void OnMouseEnter()
        {
            
            // Reproduza o som quando o mouse entrar no objeto
            audioSource.Play();
            audioSource.volume = 0.3f;

        }
    }
}
