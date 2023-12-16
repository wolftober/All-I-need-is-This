using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Shop Items")]
    public List<Sword> swords = new List<Sword>();

    [Header("Swords")]
    public GameObject swordTemplate;
    public GameObject swordContentObject;

    [Header("References")]
    public UIManager uiManager;
    public TextMeshProUGUI shopCoinsLabel;

    private int coins = 0;

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
    }

    public (bool result, int coinDiff) BuySword(string swordName)
    {
        Debug.Log("buying " + swordName);

        foreach (Sword sword in swords)
        {
            if (sword.swordName == swordName)
            {
                if (coins >= sword.cost)
                {
                    Debug.Log("bought " + swordName + "!");
                    coins -= sword.cost;
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
        }
        return (false, 0); // this would be an internal error
    }

    public void Start()
    {
        LoadItems();
    }

    public void SetCoins(int amount)
    {
        coins = amount;
    }

    // -------- Opening and Closing -------- \\

    public void OpenShop()
    {
        gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        uiManager.CoinCountChangeFromShop(coins);
    }
}
