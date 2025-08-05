using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("UI References")]
    public Slider healthBar;
    public Text healthText; // Optional: shows "75/100"
    
    [Header("Effects")]
    public GameObject damageEffect; // Optional: particle effect when damaged
    
    // Events for other systems to listen to
    public System.Action<int> OnHealthChanged;
    public System.Action OnPlayerDied;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        UpdateHealthUI();
        OnHealthChanged?.Invoke(currentHealth);
        
        // Play damage effect
        if (damageEffect != null)
        {
            Instantiate(damageEffect, transform.position, Quaternion.identity);
        }
        
        // Flash effect (optional)
        StartCoroutine(DamageFlash());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        UpdateHealthUI();
        OnHealthChanged?.Invoke(currentHealth);
    }
    
    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
        
        if (healthText != null)
        {
            healthText.text = currentHealth + "/" + maxHealth;
        }
    }
    
    System.Collections.IEnumerator DamageFlash()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }
    
    void Die()
    {
        OnPlayerDied?.Invoke();
        // Add death logic here (restart level, game over screen, etc.)
        Debug.Log("Player died!");
    }
}