using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();
        // for(int i=0; i < slotCount; i++){
        //     Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //     if(i < itemPrefabs.Length){
        //         GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //         item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //         slot.currentItem = item;
        //     }
        // }
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot.currentItem != null)
            {
            Item item = slot.currentItem.GetComponent<Item>();
            invData.Add(new InventorySaveData {itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex()} );
            }
        }
        return invData;
    } 
    
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject); // clear inventory panel, helps to avoid duplicates
        }

        for(int i=0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform); // create new slots
        }

        // populate slots with saved items
        foreach(InventorySaveData data in inventorySaveData)
        {
            
        }
    }
}

