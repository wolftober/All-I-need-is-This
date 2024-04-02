using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Template : MonoBehaviour
{
    public ShopManager shop;
    public GameObject imageObj;
    public GameObject buySection;
    public Button buyButton;
    public GameObject equipButton;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI priceLabel;

    // for buying purposes
    private string swordName;
    
    public void SetSprite(Sprite sprite)
    {
        Image image = imageObj.GetComponent<Image>();
        image.sprite = sprite;
    }

    public void SetNameLabel(string text)
    {
        nameLabel.text = text;
    }

    public void SetPriceLabel(string text)
    {
        priceLabel.text = text;
    }

    public void DisplayBuySection()
    {
        buySection.SetActive(true);
    }

    public void SetSwordName(string name)
    {
        swordName = name;
    }

    public void ShowAsEquipped()
    {
        equipButton.SetActive(true);
        Button button = equipButton.GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = "EQUIPPED";
        button.interactable = false;
    }

    public void ShowAsUnequipped()
    {
        equipButton.SetActive(true);
        Button button = equipButton.GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = "EQUIP";
        button.interactable = true;
    }

    public void Buy()
    {
        // request shop manager to buy sword, if returns true, update the template
        (bool successful, int coinsNeeded) = shop.BuySword(swordName);

        if (successful)
        {
            string newText = nameLabel.text + " (OWNED)";
            nameLabel.text = newText;

            buySection.SetActive(false);
            equipButton.SetActive(true);
        }
        else
        {
            if (coinsNeeded > 0)
            {
                Debug.Log("Coins Needed: " + coinsNeeded.ToString());
            }
            else
            {
                Debug.Log("Internal Error");
            }
        }
    }

    public void Equip()
    {
        Debug.Log("stuff");
        shop.EquipSword(swordName);
    }

    public void SetupBuyButton()
    {
        buyButton.onClick.AddListener(Buy);
    }

    public void SetupEquipButton()
    {
        equipButton.GetComponent<Button>().onClick.AddListener(Equip);
    }
}
