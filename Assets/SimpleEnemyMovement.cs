using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // How fast the enemy moves
    public float chaseRange = 5f; // Distance to start chasing

    private Transform player; // Reference to the player
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has "Player" tag
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            // Move toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // Stop moving if out of range
            rb.velocity = Vector2.zero;
        }
    }
}