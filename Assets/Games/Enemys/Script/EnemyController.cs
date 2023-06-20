using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Helth")]
    [Tooltip("enemy's current health")]
    [SerializeField] private int currentHealth;

    [Tooltip("Enemy maximum health")]
    [SerializeField] private int maxHealth = 100;

    [Tooltip("Reference to the life bar image")]
    [SerializeField] private Image healthBarImage;

    private Animator animator;

    private int _animationEnemy = Animator.StringToHash("DeathEnemy");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Garante que a vida nunca seja menor que zero
        currentHealth = Mathf.Max(0, currentHealth);

        float healthRatio = (float)currentHealth / maxHealth;
        healthBarImage.fillAmount = healthRatio;

        if (currentHealth <= 0f)
        {
            Die();
            StartCoroutine(WaitANimationEnemyDeath());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CajadoHughan"))
        {
            int damage = 50;
            TakeDamage(damage);
        }
    }

    private void Die()
    {
        animator.SetBool(_animationEnemy, true);
    }

    private IEnumerator WaitANimationEnemyDeath()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
