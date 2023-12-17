using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
    public Transform itemSlots;

    public void SwitchTo(int boxIndex)
    {
        Transform targetBox = itemSlots.GetChild(boxIndex);

        if (targetBox.childCount != 0)
        {
            Item item = targetBox.GetChild(0).GetComponent<Item>();

            item.Selected();
        }
    }
}
