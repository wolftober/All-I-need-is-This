using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEntry : MonoBehaviour
{
    // THIS SCRIPT is added to a game object (template) for interaction with shop manager
    private string itemCategory; // ex: Sword, Potion, Power Up --> this separates item into a category for shop manager
    private string itemName; // ex: Fire Sword (must match the shop selection dictionary key)

    private bool isOwnable = false; // if true, tells the entry to perform ownership function when bought

    [Header("Entry References")]
    public ShopManager shop;
    public GameObject imageObject;
    public GameObject buySection;
    public Button buyButton;
    public TextMeshProUGUI nameLabel; // item title
    public TextMeshProUGUI priceLabel;
    public TextMeshProUGUI description;

    public void BuyItem()
    {
        (bool successful, int coinsNeeded) = shop.PurchaseItem(itemCategory, itemName, 1);
        if (successful)
        {
            Debug.Log("Purchase successful!");
            // apply indicator to user or something
        }
        else
        {
            if (coinsNeeded > 0)
            {
                Debug.Log($"Need {coinsNeeded} more coins...");
            }
            else
            {
                Debug.Log("Internal Error");
            }
        }
    }

    public void ConnectBuyButton()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    public void Setup(Item item)
    {
        imageObject.GetComponent<Image>().sprite = item.GetSprite(); // icon
        nameLabel.text = item.itemName; // name label

        nameLabel.color = item.nameLabelColor;

        priceLabel.text = item.price.ToString(); // price label
        isOwnable = item.isOwnable; // is this item ownable?
        itemCategory = item.itemCategory; // category
        itemName = item.itemName; // item name, used for shop 
        description.text = item.itemDescription;

        gameObject.SetActive(true);
        buySection.SetActive(true);

        ConnectBuyButton();
    }
}
