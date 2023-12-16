using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemBaseObject;
    public Transform itemSlots;

    // returns false if there is no space to add item
    public bool AddItem(Item item)
    {
        // find first available slot (no stacking yet)
        foreach (Transform slot in itemSlots)
        {
            if (slot.childCount == 0)
            {
                Debug.Log($"Placing in {slot.gameObject.name}");
                GameObject itemObject = Instantiate(itemBaseObject, slot);

                itemObject.GetComponent<Image>().sprite = item.GetSprite();

                itemObject.SetActive(true);

                return true;
            }
        }
        return false;
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        // this is where extra closing steps would be done

        gameObject.SetActive(false);
    }
}
