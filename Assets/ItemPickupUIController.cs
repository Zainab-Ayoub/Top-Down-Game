using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;   
using System.Collections;

public class ItemPickupUIController : MonoBehaviour
{
    public static ItemPickupUIController Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopups = 5;
    public float popupDuration = 3f;

    private readonly Queue<GameObject> activePopups = new();

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

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon").GetComponent<Image>();

        if(itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        activePopups.Enqueue(newPopup);
        if(activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }
        // fade out & destroy
        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);

        if (popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = popup.AddComponent<CanvasGroup>();
        }

        for (float timePassed = 0f; timePassed < 1f; timePassed += Time.deltaTime)
        {
            if (popup == null) yield break;

            canvasGroup.alpha = 1f - timePassed;
            yield return null;
        }

        Destroy(popup);
    }

}
