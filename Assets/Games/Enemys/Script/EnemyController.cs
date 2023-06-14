using UnityEngine.UI;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CajadoHughan"))
        {
            int damage = 10;
            TakeDamage(damage);
        }
    }
}
