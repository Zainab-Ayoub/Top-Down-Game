using UnityEngine;
using Cinemachine
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData{
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindGameObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }
}
