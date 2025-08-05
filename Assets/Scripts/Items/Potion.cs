using UnityEngine;

public class Potion : MonoBehaviour
{
    [Header("Potion Settings")]
    public int healAmount = 25;
    public AudioClip collectSound;
    public GameObject collectEffect;
    
    [Header("Animation")]
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;
    
    private Vector3 startPos;
    private AudioSource audioSource;
    
    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void Update()
    {
        // Bob up and down animation
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
        
        // Rotate slowly
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                
                // Play sound effect
                if (collectSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(collectSound);
                }
                
                // Spawn collect effect
                if (collectEffect != null)
                {
                    Instantiate(collectEffect, transform.position, Quaternion.identity);
                }
                
                // Hide visuals but keep audio playing
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                
                // Destroy after sound finishes
                Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f);
            }
        }
    }
}