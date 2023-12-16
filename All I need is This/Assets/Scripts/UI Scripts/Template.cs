using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Template : MonoBehaviour
{
    public GameObject imageObj;
    public GameObject buySection;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI priceLabel;
    
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
}
