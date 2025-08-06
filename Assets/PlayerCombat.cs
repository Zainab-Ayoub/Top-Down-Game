using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public Transform attackPoint;
    public ParticleSystem attackParticle; // Add this field

    void Start()
    {
        if (attackParticle == null)
        {
            attackParticle = transform.Find("AttackParticle")?.GetComponent<ParticleSystem>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play particle effect
        if (attackParticle != null)
        {
            attackParticle.Play();
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(1);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}