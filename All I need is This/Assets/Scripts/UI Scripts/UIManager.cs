using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject gameOverPanelHeading;

    // for coin updates
    public TextMeshProUGUI playerCoinLabel;
    public TextMeshProUGUI shopCoinLabel;

    public ShopManager shop;
    private bool shopOpen = false;

    // these are the various different game over lines the player can see
    List<string> gameOverLines = new List<string>(new string[] {
        "You died!",
        "Seems like your journey has reached its end.",
        "Better luck next time.",
        "Oops!",
        "You have died..."
    });

    private string GetRandGameOverLine()
    {
        string randomLine = gameOverLines[Random.Range(0, gameOverLines.Count)];
        return randomLine;
    }

    // Called by outside functions to display the game over menu
    public void OpenGameOverMenu()
    {
        // get a random game over line to display on the window
        string randomGameOverLine = GetRandGameOverLine();

        gameOverPanelHeading.GetComponent<TextMeshProUGUI>().text = randomGameOverLine;
        gameOverPanel.SetActive(true);
    }

    public void UpdateCoinCount(int newCount)
    {
        string coinsAmount = newCount.ToString();

        // update the player display
        playerCoinLabel.text = coinsAmount;

        // update the shop display
        shopCoinLabel.text = coinsAmount;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (shopOpen)
            {
                shop.CloseShop();
                shopOpen = false;
            }
            else
            {
                shop.OpenShop();
                shopOpen = true;
            }
        }
    }
}
