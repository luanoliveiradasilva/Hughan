using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Games.Character.Scripts
{
    public class HelthDamage : MonoBehaviour
    {
        [Header("Helth")]
        [Tooltip("Maximum living condition")]
        public int maxHealth = 100;
        [Tooltip("Current health condition")]
        public int currentHealth;

        // Referência para a imagem da barra de vida
        [Header("Helthbar")]
        [Tooltip("Reference to the life bar image")]
        public Image healthBarImage;

        private Animator animator;
        public Vector3 respawnPosition;
        private CharacterController characterController;

        private int _animationPlayerDeath = Animator.StringToHash("Death");

        private bool deathPlayer = false;

        private void Start()
        {
            currentHealth = maxHealth;
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            respawnPosition = transform.position;
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
            }
        }

        private void UpdateHealthBar()
        {
            float healthRatio = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = healthRatio;
        }

        private void Die()
        {
            // Lança um raio abaixo do personagem para detectar o chão
            Ray groundRay = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            int terrainLayerMask = LayerMask.GetMask("Terrain");
            // Verifica se o raio colide com o chão
            if (Physics.Raycast(groundRay, out hit, Mathf.Infinity, terrainLayerMask))
            {
                // Ajuste de altura para a posição de queda
                float heightOffset = 0.1f;

                // Move o personagem para a posição de queda, considerando a altura do chão
                Vector3 fallPosition = hit.point - new Vector3(0f, heightOffset, 0f);
                transform.position = fallPosition;

                animator.SetBool(_animationPlayerDeath, true);
            }           
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                int damage = 20;
                TakeDamage(damage);
            }
        }
    }
}
