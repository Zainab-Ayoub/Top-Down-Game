using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        for(int i=0; i < slotCount; i++){
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if(i < itemPrefabs.Length){
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPane.transform)
        {
            Slot slot = slotTransform.Getcomponent<Slot>();
            if(Slot.currentItem ! = null)
            {
            Item item = slot.currentItem.Getcomponent<Slot>();
            invData.Add(new InventorySaveData {item.ID, slotIndex = slotTransform.GetSiblingIndex()} );
            }
        }
        return invData;
    } 
    
}

