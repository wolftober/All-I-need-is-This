using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    // when something is dropped on this object
    public void OnDrop(PointerEventData eventData)
    {
        // this check should be able to be avoided for stackable items
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>(); // the script on the item

            draggableItem.parentAfterDrag = transform; // setting the item's new parent
        }
    }
}
