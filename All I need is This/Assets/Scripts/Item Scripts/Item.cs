using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemCategory;
    public string itemName;

    public Color nameLabelColor = Color.black;

    [Multiline]
    public string itemDescription;

    public int price;

    public bool isOwnable;

    public GameObject image;

    public Sprite GetSprite()
    {
        return image.GetComponent<Image>().sprite;
    }

    // this should be overriden by various items to give them functionality
    public virtual void Selected()
    {
        return;
    }
}
