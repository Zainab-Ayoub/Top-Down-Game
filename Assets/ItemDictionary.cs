using UnityEngine;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;
 
    public void Awake()
    {
        itemDictionary = new Dictionary<int, GameObject>();
        // auto increment ids
        for(int i=0; i < itemPrefabs.Count; i++)
        {
            if(itemPrefabs[i] != null)
            {
                itemPrefabs[i].ID = i+1;
            }
        }
        
        foreach(Item item in itemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if(prefab == null)
        {
            Debug.LogWarning($"Item with ID {itemID} not found in dictionary.");
        }
        return prefab;
    }
}
