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

    public PlayerManager playerManager;
    public ShopManager shop;
    public InventoryManager inventory;
    public HotbarManager hotbar;

    private bool inventoryOpen = false;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            shop.Toggle();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                inventory.CloseInventory();
            }
            else
            {
                inventory.OpenInventory();
            }

            inventoryOpen = !inventoryOpen;
        }

        // hotbar
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hotbar.InteractWith(0); // box at index 0
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotbar.InteractWith(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hotbar.InteractWith(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hotbar.InteractWith(3);
        }
    }
}
