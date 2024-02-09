using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;

    public enum itemCat
    {potions, other}

    public itemCat itemCategory;

    public Color nameLabelColor = Color.black;

    [Multiline]
    public string itemDescription;

    public int price;

    public bool isOwnable;

    public Sprite icon;

    [HideInInspector]
    public PlayerManager player;

    public bool displayInShop = true; // will this item be available for purchase immediately?
    public bool isLocked = false;

    // this should be overriden by various items to give them functionality
    public virtual void Selected()
    {
        Debug.Log("Base selected method has been used.");
    }
}

public interface IDuration
{
    public float duration { get; }

    public void DurationEnded();
}
