using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    public List<Sword> swords = new List<Sword>();

    [Header("Swords")]
    public GameObject swordTemplate;
    public GameObject swordContentObject;

    private void LoadItems()
    {
        // Sword Entries
        foreach (Sword sword in swords)
        {
            // instantiating the template
            GameObject template = Instantiate(swordTemplate, swordContentObject.transform);
            Template swordDisplay = template.GetComponent<Template>();

            // setting it up
            swordDisplay.SetSprite(sword.getSwordSprite());

            if (sword.owned)
            {
                swordDisplay.SetNameLabel(sword.swordName + " (OWNED)");
            }
            else
            {
                swordDisplay.SetNameLabel(sword.swordName);

                swordDisplay.SetPriceLabel(sword.cost.ToString());
                swordDisplay.DisplayBuySection();
            }

            // activating the template
            template.SetActive(true);
        }
    }

    public void Start()
    {
        LoadItems();
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
