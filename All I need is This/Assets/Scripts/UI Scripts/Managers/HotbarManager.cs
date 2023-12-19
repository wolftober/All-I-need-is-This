using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public Transform itemSlots;

    public ItemsManager items;

    public void InteractWith(int boxIndex)
    {
        Transform targetBox = itemSlots.GetChild(boxIndex);

        if (targetBox.childCount != 0)
        {
            GameObject holderObject = targetBox.GetChild(0).gameObject;

            (bool exists, Item item) = items.GetItem(holderObject.name);

            item.Selected();

            Destroy(holderObject);
        }
    }
}
