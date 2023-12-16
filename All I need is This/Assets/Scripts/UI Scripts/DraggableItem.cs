using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parentAfterDrag; // original parent, can change when dropped in new location

    public Image image;

    // called when mouse begins dragging the item
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        // making it so that the item is on the top layer, viewable at all times
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // this is to prevent the mouse from being unable to see the new inventory slot to place the item in
        image.raycastTarget = false;
    }

    // called while mouse moves while dragging the item
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // follow the mouse
    }

    // called when the item is dropped
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        image.raycastTarget = true;
    }
}
