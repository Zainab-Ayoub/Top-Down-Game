using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;   

public class ItemPickupUIController : MonoBehaviour
{
    public static ItemPickupUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration;

    private readonly Queue<GameObject> activePopup = new();

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } 
        else
        {
            Debug.LogError("Multiple ItemPickupUIManager instances detected! Destroying the extra one.");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon").GetComponent<Image>();

        if(itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        activePopups.Enqueue(newPopup);
    }
}
