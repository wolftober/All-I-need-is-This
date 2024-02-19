using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IOpenAndClose
{
    public GameObject itemObjectReference; // for cloning
    public Transform itemSlots;

    public bool inventoryOpen = false;

    // returns false if there is no space to add item
    public bool AddItem(Item item)
    {
        // find first available slot (no stacking yet)
        foreach (Transform slot in itemSlots)
        {
            if (slot.childCount == 0)
            {
                Debug.Log($"Placing in {slot.gameObject.name}");
                GameObject itemObject = Instantiate(itemObjectReference, slot);
                itemObject.name = item.itemName;

                itemObject.GetComponent<Image>().sprite = item.icon;

                // adding dragability to the item
                itemObject.GetComponent<DraggableItem>().image = itemObject.GetComponent<Image>();

                itemObject.SetActive(true);

                return true;
            }
        }
        return false;
    }

    public bool Toggle()
    {
        if (inventoryOpen)
        {
            CloseInventory();
            inventoryOpen = !inventoryOpen;
            return false;
        }
        else
        {
            OpenInventory();
            inventoryOpen = !inventoryOpen;
            return true;
        }
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
