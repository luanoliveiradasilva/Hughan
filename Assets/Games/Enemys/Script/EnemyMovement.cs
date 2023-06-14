using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // Reference to the target (player) transform
    public float movementSpeed = 5f; // Speed at which the enemy moves

    private void Start()
    {
        FindTarget(); // Call FindTarget when the enemy starts to set the initial target
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        if (target != null)
        {
            // Calculate the direction from the enemy to the target
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // Move the enemy towards the target
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
        else
        {
            FindTarget(); // Call FindTarget to restore the target reference
        }
    }

    private void FindTarget()
    {
        // Find the player or set a new target based on your game logic
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SetTarget(player.transform);
        }
    }
}
