using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IOpenAndClose
{
    bool Toggle();
}

public class UIManager : MonoBehaviour
{
    public PlayerData playerData;

    public GameObject gameOverPanel;
    public GameObject gameOverPanelHeading;

    // for coin updates
    public TextMeshProUGUI playerCoinLabel;

    public PlayerManager playerManager;
    public ShopManager shop;
    public InventoryManager inventory;
    public HotbarManager hotbar;

    [HideInInspector]
    public List<IOpenAndClose> openedWindows = new List<IOpenAndClose>();

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

    public void UpdateCoinCount()
    {
        playerCoinLabel.text = playerData.coins.ToString();
    }

    private void Start()
    {
        UpdateCoinCount();
    }

    public void RemoveFromOpenedWindows(IOpenAndClose window)
    {
        openedWindows.Remove(window);
    }

    public void Update()
    {
        // UI keybinds
        if (Input.GetKeyDown(KeyCode.G))
        {
            // cant open if other stuff is already open
            if (!shop.shopOpen && openedWindows.Count > 0)
            {
                return;
            }

            bool opened = shop.Toggle(); // true means add to the list

            if (opened)
                openedWindows.Add(shop);
            else
                RemoveFromOpenedWindows(shop);

            Debug.Log(openedWindows.Count);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.inventoryOpen && openedWindows.Count > 0)
            {
                return;
            }

            bool opened = inventory.Toggle();

            if (opened)
                openedWindows.Add(inventory);
            else
                openedWindows.Remove(inventory);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && openedWindows.Count > 0)
        {
            IOpenAndClose window = openedWindows[openedWindows.Count - 1];
            window.Toggle();
            openedWindows.Remove(window);
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
