using UnityEngine;

namespace Assets.MainMenu.Scripts
{
    class SoundMouse : MonoBehaviour
    {

        public AudioClip soundClip;
        private AudioSource audioSource;

        private void Start()
        {
            // Adicione um componente AudioSource ao objeto que possui esse script
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.3f;
        }

        private void OnMouseEnter()
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Entrou");
            // Verificar se o trigger ocorreu com o objeto desejado (o botão)
            if (collision.CompareTag("Cursor"))
            {
                audioSource.clip = soundClip;
                audioSource.Play();
            }
        }
    }      
}
