using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemCategory;
    public string itemName;

    [Multiline]
    public string itemDescription;

    public int price;

    public bool isOwnable;

    public GameObject image;

    public Sprite GetSprite()
    {
        return image.GetComponent<Image>().sprite;
    }
}
