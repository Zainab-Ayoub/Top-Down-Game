using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float moveSpeed = 2f;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;
    
    [Header("References")]
    public Transform player;
    public Animator animator;
    
    private Rigidbody2D rb;
    private float lastAttackTime;
    private bool playerInRange = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Find player if not assigned
        if (player == null)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null)
            {
                player = playerGO.transform;
            }
        }
    }
    
    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= chaseRange)
        {
            if (distanceToPlayer <= attackRange)
            {
                // Stop moving and attack
                rb.velocity = Vector2.zero;
                AttackPlayer();
            }
            else
            {
                // Chase player
                ChasePlayer();
            }
        }
        else
        {
            // Stop moving
            rb.velocity = Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }
    
    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        
        // Flip sprite based on movement direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
        // Set animation
        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
        }
    }
    
    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            
            // Trigger attack animation
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
            
            // Deal damage to player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
    
    // Visualize ranges in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}