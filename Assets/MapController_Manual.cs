using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MapController_Manual : MonoBehaviour
{
    public static MapController_Manual Instance{ get; set; }

    public GameObject mapParent;
    List<Image> mapImages;

    public Color highlightColour = Color.yellow;
    public Color dimedColour = new Color(1f, 1f, 1f, 0.5f); 

    public RectTransform playerIconTransform;

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        mapImages = mapParent.GetComponentInChildren<Image>().ToList();
    }
}