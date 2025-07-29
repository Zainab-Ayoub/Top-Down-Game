using UnityEngine;
using UnityEngine.EventSystem;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start(){
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        originalParent = transform.parent // save og parent
        transform.SetParent(transform.root); // above other canvas'
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // semi-transparent during drag
    }

    public void OnDrag(PointerEventData eventData){
        transform.position = eventData.position; // follow the mouse
    }

    public void OnEndDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = true; // enables raycasts
        canvasGroup.alpha = 1f; // no longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // slot where item is dropped
        Slot originalSlot = originalParent.GetComponent<Slot>();

        if(dropSlot != null){
            if(dropSlot.currentItem != null){ // is a slot under drop point
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            } 
            else{
                originalSlot.currentItem = null;
            }

            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        } 
        else{
            transform.SetParent(originalParent); // no slot under drop point
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // center
    }
}