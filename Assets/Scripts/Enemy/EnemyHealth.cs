using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    public int currentHealth;
    
    [Header("UI")]
    public Canvas healthBarCanvas;
    public Slider healthBarSlider;
    
    [Header("Drops")]
    public GameObject[] possibleDrops; // Array of potion prefabs
    public float dropChance = 0.3f; // 30% chance to drop something
    
    [Header("Effects")]
    public GameObject deathEffect;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        
        // Make health bar face camera
        if (healthBarCanvas != null)
        {
            healthBarCanvas.worldCamera = Camera.main;
        }
    }
    
    void Update()
    {
        // Keep health bar facing camera
        if (healthBarCanvas != null && Camera.main != null)
        {
            healthBarCanvas.transform.LookAt(Camera.main.transform);
            healthBarCanvas.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
        }
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        UpdateHealthBar();
        
        // Flash effect
        StartCoroutine(DamageFlash());
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = (float)currentHealth / maxHealth;
            
            // Hide health bar when at full health
            healthBarCanvas.gameObject.SetActive(currentHealth < maxHealth);
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
        // Spawn death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        // Drop items
        if (possibleDrops.Length > 0 && Random.value < dropChance)
        {
            int randomDrop = Random.Range(0, possibleDrops.Length);
            Instantiate(possibleDrops[randomDrop], transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}