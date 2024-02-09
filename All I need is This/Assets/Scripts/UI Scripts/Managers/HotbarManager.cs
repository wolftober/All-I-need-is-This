using System;
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

            IDuration itemWithDuration = null;
            try
            {
                itemWithDuration = (IDuration) item;
            }
            catch (InvalidCastException)
            {
                Debug.Log("Invalid Cast, no worries, everything is under control.");
            }
            if (itemWithDuration != null)
            {
                StartCoroutine(WaitThenEndDuration(itemWithDuration.duration, itemWithDuration));
            }

            Destroy(holderObject);
        }
    }

    private IEnumerator WaitThenEndDuration(float duration, IDuration item)
    {
        yield return new WaitForSeconds(duration);

        item.DurationEnded();

        Debug.Log("Back to normal");
    }
}
