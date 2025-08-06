using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; // 3 hits to kill
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy
        Destroy(gameObject);
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        // Check if all enemies are defeated
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Debug.Log("All enemies defeated! You Win!");
            // Add win UI or scene transition here (see Step 5)
        }
    }
}