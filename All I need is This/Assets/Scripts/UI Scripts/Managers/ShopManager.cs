using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShopManager : MonoBehaviour
{
    //[Header("Shop Items")]
    private ItemsManager itemsManager;

    // swords
    public List<Sword> swords = new List<Sword>();
    private Dictionary<string, Sword> swordSelection = new Dictionary<string, Sword>();

    [Header("Swords")]
    public GameObject swordTemplate;
    public GameObject swordContentObject;

    [Header("Items")]
    public GameObject itemTemplate;
    public GameObject itemsContentObject;

    [Header("References")]
    public GameObject shopContent;
    public PlayerData playerData;
    public UIManager uiManager;
    public InventoryManager inventory;
    public TextMeshProUGUI shopCoinsLabel;
    public TextMeshProUGUI bottomUICoinLabel;

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
        foreach (Item item in itemsManager.GetAllItems())
        {
            if (item.displayInShop)
            {
                GameObject template = Instantiate(itemTemplate, itemsContentObject.transform);
                ShopEntry entry = template.GetComponent<ShopEntry>();

                entry.Setup(item); // passing the item to entry, all setup should be done in the entry script
            }
        }
    }

    public (bool result, int coinDiff) BuySword(string swordName)
    {
        Debug.Log("buying " + swordName);

        if (swordSelection.ContainsKey(swordName))
        {
            Sword sword = swordSelection[swordName];

            if (playerData.coins >= sword.cost)
            {
                Debug.Log("bought " + swordName + "!");
                playerData.coins -= sword.cost;
                
                Debug.Log("Coins now at: " + playerData.coins);

                // updating shop coin label
                shopCoinsLabel.text = playerData.coins.ToString();

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

        (bool itemExists, Item item) = GameObject.FindGameObjectWithTag("Items Manager").GetComponent<ItemsManager>().GetItem(name);

        if (itemExists)
        {
            int fullPrice = item.price * quantity;

            if (playerData.coins >= fullPrice)
            {
                Debug.Log("bought " + name + "!");
                playerData.coins -= item.price;

                // send (item) x(quantity) to player inventory
                inventory.AddItem(item);

                Debug.Log("Coins now at: " + playerData.coins);

                shopCoinsLabel.text = playerData.coins.ToString();
                bottomUICoinLabel.text = playerData.coins.ToString();

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

    // -------- Runtime Updates -------- \\

    public void UpdateCoinCount()
    {
        shopCoinsLabel.text = playerData.coins.ToString();
    }

    public void Start()
    {
        itemsManager = GameObject.FindGameObjectWithTag("Items Manager").GetComponent<ItemsManager>();
        Debug.Log(itemsManager);

        UpdateCoinCount();
        LoadItems();
        SetupShopSelections();
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
        shopContent.SetActive(true);
    }

    public void CloseShop()
    {
        shopContent.SetActive(false);
    }
}
