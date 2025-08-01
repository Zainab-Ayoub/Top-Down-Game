using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class SaveData{
    public Vector3 playerPosition;
    public string mapBoundary; // the boundary name for the map
    public List<InventorySaveData> inventorySaveData;
}
