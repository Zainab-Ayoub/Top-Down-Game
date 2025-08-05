using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Explosion Settings")]
    public int explosionDamage = 35;
    public float explosionRadius = 2f;
    public float explosionCooldown = 1f;
    public LayerMask enemyLayers;
    
    [Header("Explosion Effects")]
    public GameObject explosionPrefab; // Drag explosion effect here
    public AudioClip explosionSound;
    public float screenShakeIntensity = 0.3f;
    public float screenShakeDuration = 0.2f;
    
    [Header("Visual Feedback")]
    public float chargeTime = 0.5f; // Time to charge before explosion
    public GameObject chargingEffect; // Optional charging effect
    
    private float nextExplosionTime = 0f;
    private bool isCharging = false;
    private AudioSource audioSource;
    private Camera mainCamera;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        HandleCombatInput();
    }
    
    void HandleCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextExplosionTime && !isCharging)
        {
            StartCoroutine(ChargeExplosion());
            nextExplosionTime = Time.time + explosionCooldown;
        }
    }
    
    System.Collections.IEnumerator ChargeExplosion()
    {
        isCharging = true;
        
        // Show charging effect
        GameObject chargingFX = null;
        if (chargingEffect != null)
        {
            chargingFX = Instantiate(chargingEffect, transform.position, Quaternion.identity, transform);
        }
        
        // Wait for charge time
        yield return new WaitForSeconds(chargeTime);
        
        // Clean up charging effect
        if (chargingFX != null)
        {
            Destroy(chargingFX);
        }
        
        // Create explosion
        CreateExplosion();
        
        isCharging = false;
    }
    
    void CreateExplosion()
    {
        // Spawn explosion visual effect
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            // Scale explosion based on radius
            explosion.transform.localScale = Vector3.one * (explosionRadius / 1f);
            
            // Destroy explosion effect after animation
            Destroy(explosion, 2f);
        }
        
        // Play explosion sound
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        
        // Screen shake
        if (mainCamera != null)
        {
            StartCoroutine(ScreenShake());
        }
        
        // Detect and damage enemies in explosion radius
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
                
                // Add explosion knockback
                Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    float knockbackForce = Mathf.Lerp(800f, 200f, distance / explosionRadius);
                    
                    enemyRb.AddForce(knockbackDirection * knockbackForce);
                }
            }
        }
        
        Debug.Log($"Explosion hit {hitEnemies.Length} enemies!");
    }
    
    System.Collections.IEnumerator ScreenShake()
    {
        Vector3 originalPosition = mainCamera.transform.position;
        float elapsed = 0f;
        
        while (elapsed < screenShakeDuration)
        {
            float x = Random.Range(-screenShakeIntensity, screenShakeIntensity);
            float y = Random.Range(-screenShakeIntensity, screenShakeIntensity);
            
            mainCamera.transform.position = originalPosition + new Vector3(x, y, 0);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        mainCamera.transform.position = originalPosition;
    }
    
    // Show explosion radius in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        
        if (isCharging)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, explosionRadius * 0.5f);
        }
    }
}