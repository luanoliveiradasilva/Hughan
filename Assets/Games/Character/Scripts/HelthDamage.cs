using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Games.Character.Scripts
{
    public class HelthDamage : MonoBehaviour
    {
        [Header("Helth")]
        [Tooltip("Maximum living condition")]
        [SerializeField] private int maxHealth = 100;
        [Tooltip("Current health condition")]
        [SerializeField] private int currentHealth;

        // Referência para a imagem da barra de vida
        [Header("Helthbar")]
        [Tooltip("Reference to the life bar image")]
        [SerializeField] private Image healthBarImage;

        private int _animationPlayerDeath = Animator.StringToHash("Death");

        private Animator animator;
        private CharacterController characterController;
        [SerializeField] private GameObject _gameOver;

        private bool deathPlayer = false;        

        private void Awake()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            // Garante que a vida nunca seja menor que zero
            currentHealth = Mathf.Max(0, currentHealth);

            // Atualiza a barra de vida
            UpdateHealthBar();

            if (currentHealth == 0 && !deathPlayer)
            {

                Die();
               StartCoroutine(waitForDeath());
            }
        }

        private void UpdateHealthBar()
        {
            float healthRatio = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = healthRatio;
        }

        private void Die()
        {     
            animator.SetBool(_animationPlayerDeath, true);
            StartCoroutine(IncreaseCenterY(3.8f));
        }

        private IEnumerator waitForDeath()
        {
            yield return new WaitForSeconds(3);
            Time.timeScale = 0f;
            _gameOver.SetActive(true);
        }

        //Calcula do collider padrao do character controller diminui o position center Y
        private IEnumerator IncreaseCenterY(float duration)
        {

            Vector3 initialCenter = characterController.center;
            Vector3 targetCenter = initialCenter;
            targetCenter.y += 1.1f; // Ajuste o valor conforme necessário

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float time = Mathf.Clamp01(elapsedTime / duration);
                characterController.center = Vector3.Lerp(initialCenter, targetCenter, time);
                yield return null;
            }

            // Garanta que o valor final seja exatamente o desejado
            characterController.center = targetCenter;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                int damage = 50;
                TakeDamage(damage);
            }
        }
    }
}
