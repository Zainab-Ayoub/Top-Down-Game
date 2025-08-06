using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if(item != null)
            {
                // Try to add item to inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);

                if(itemAdded)
                {
                    item.PickUp();

                    // Heal instantly if it's a potion
                    if(item.Name.ToLower().Contains("potion"))
                    {
                        PlayerHealthBar playerHealth = GetComponent<PlayerHealthBar>();
                        if(playerHealth != null)
                        {
                            playerHealth.Heal(2); // change 2 to desired heal amount
                        }
                    }

                    // Remove item from scene after pickup
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
