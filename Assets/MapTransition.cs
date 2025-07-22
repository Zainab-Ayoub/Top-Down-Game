using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    [SerializeField] Direction direction;
    // [SerializeField] float additivePos = 2f;

    enum Direction { Up, Down, Left, Right };

    private void Awake(){
        confiner = UnityEngine.Object.FindFirstObjectByType<CinemachineConfiner2D>();
    }     

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void 
}
