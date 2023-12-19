using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Item Manager")]
public class ItemsManager : MonoBehaviour
{
    [Header("Item Setup")]
    public PlayerManager playerManager;

    public List<Item> items = new List<Item>();
    private Dictionary<string, Item> itemsMapping = new Dictionary<string, Item>();

    private void Start()
    {
        foreach (Item item in items)
        {
            // setting up the item
            item.player = playerManager;

            itemsMapping.Add(item.itemName, item);
        }

        Debug.Log("Done setting up items");
    }

    public (bool itemExists, Item item) GetItem(string itemName)
    {
        if (itemsMapping.ContainsKey(itemName))
        {
            return (true, itemsMapping[itemName]);
        }
        else
        {
            return (false, null);
        }
    }

    public List<Item> GetAllItems()
    {
        return items;
    }
}
