using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<Sword> swords = new List<Sword>();

    private void LoadItems()
    {
        // the entries
        foreach (Sword sword in swords)
        {
            string exampleEntry = "";
            exampleEntry += sword.swordName;
            if (sword.owned)
            {
                exampleEntry += " (Owned)";
            }
            else
            {
                exampleEntry += $" Costs {sword.cost}";
            }
            Debug.Log(exampleEntry);
        }
    }

    public void OpenShop()
    {
        LoadItems();
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
