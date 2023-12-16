using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    public List<Sword> swords = new List<Sword>();
    private Dictionary<string, Sword> swordSelection = new Dictionary<string, Sword>();

    public List<Item> items = new List<Item>();
    private Dictionary<string, Item> itemSelection = new Dictionary<string, Item>();

    [Header("Swords")]
    public GameObject swordTemplate;
    public GameObject swordContentObject;

    [Header("Items")]
    public GameObject itemTemplate;
    public GameObject itemsContentObject;

    [Header("References")]
    public UIManager uiManager;
    public InventoryManager inventory;
    public TextMeshProUGUI shopCoinsLabel;

    private int coins = 0;

    private bool shopOpen = false;

    // this sets item's name --> item script
    // this is so that it can be bought later
    private void SetupShopSelections()
    {
        // swords
        foreach (Sword sword in swords)
        {
            swordSelection.Add(sword.swordName, sword);
        }

        // items, category sorting should be implemented later
        foreach (Item item in items)
        {
            itemSelection.Add(item.itemName, item);
        }
    }

    private void LoadItems()
    {
        // Sword Entries
        foreach (Sword sword in swords)
        {
            // instantiating the template
            GameObject template = Instantiate(swordTemplate, swordContentObject.transform);
            Template swordDisplay = template.GetComponent<Template>();

            // setting it up
            swordDisplay.SetSwordName(sword.swordName);
            swordDisplay.SetSprite(sword.GetSwordSprite());

            if (sword.owned)
            {
                swordDisplay.SetNameLabel(sword.swordName + " (OWNED)");
            }
            else
            {
                swordDisplay.SetNameLabel(sword.swordName);

                swordDisplay.SetPriceLabel(sword.cost.ToString());
                swordDisplay.DisplayBuySection();

                swordDisplay.SetupBuyButton();
            }

            // activating the template
            template.SetActive(true);
        }

        // Item Entries
        foreach (Item item in items)
        {
            GameObject template = Instantiate(itemTemplate, itemsContentObject.transform);
            ShopEntry entry = template.GetComponent<ShopEntry>();

            entry.Setup(item); // passing the item to entry, all setup should be done in the entry script
        }
    }

    public (bool result, int coinDiff) BuySword(string swordName)
    {
        Debug.Log("buying " + swordName);

        if (swordSelection.ContainsKey(swordName))
        {
            Sword sword = swordSelection[swordName];

            if (coins >= sword.cost)
            {
                Debug.Log("bought " + swordName + "!");
                coins -= sword.cost;
                uiManager.CoinCountChangeFromShop(coins);
                Debug.Log("Coins now at: " + coins);

                // updating shop coin label
                shopCoinsLabel.text = coins.ToString();

                return (true, 0);
            }
            else
            {
                Debug.Log("Insufficient Funds!");
                return (false, sword.cost - coins);
            }
        }
        else
        {
            return (false, 0); // this would be an internal error
        }
    }

    public (bool result, int coinDiff) PurchaseItem(string category, string name, int quantity)
    {
        Debug.Log($"buying '{name}' in category '{category}'");

        if (itemSelection.ContainsKey(name))
        {
            Item item = itemSelection[name];

            int fullPrice = item.price * quantity;

            if (coins >= fullPrice)
            {
                Debug.Log("bought " + name + "!");
                coins -= item.price;

                // send (item) x(quantity) to player inventory
                inventory.AddItem(item);

                uiManager.CoinCountChangeFromShop(coins);
                Debug.Log("Coins now at: " + coins);

                shopCoinsLabel.text = coins.ToString();

                return (true, 0);
            }
            else
            {
                return (false, item.price - coins);
            }
        }
        else
        {
            return (false, 0); // this would be an internal error
        }
    }

    public void Start()
    {
        LoadItems();
        SetupShopSelections();
    }

    public void SetCoins(int amount)
    {
        coins = amount;
    }

    // -------- Opening and Closing -------- \\

    public void Toggle()
    {
        if (shopOpen)
        {
            CloseShop();
        }
        else
        {
            OpenShop();
        }

        shopOpen = !shopOpen;
    }

    public void OpenShop()
    {
        coins = uiManager.RequestCoinCount();
        shopCoinsLabel.text = coins.ToString();

        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);

        uiManager.CoinCountChangeFromShop(coins);
    }
}
