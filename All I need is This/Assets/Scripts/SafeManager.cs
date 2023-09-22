using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public int coins = 500;
    public TextMesh coinTextMesh;

    public void takeCoins(int amount)
    {
        coins -= amount;
        updateCoinDisplay();
    }

    public void addCoins(int amount)
    {
        coins += amount;
        updateCoinDisplay();
    }

    // updates the coin display to show the current coin amount
    private void updateCoinDisplay()
    {
        coinTextMesh.text = coins.ToString();
    }
}
