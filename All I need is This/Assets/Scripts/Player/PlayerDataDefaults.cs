using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerDataDefaults : MonoBehaviour
{
    public PlayerData playerData;

    public float defaultHealth = 200f;
    public int defaultCoins = 300;
    public int startingXpLevel = 0;
    public int defaultXpPoints = 0;
    public int defaultXpPointsNeeded = 10;

    private void Start()
    {
        playerData.playerHealth = defaultHealth;
        playerData.coins = defaultCoins;
        playerData.xpLevel = startingXpLevel;
        playerData.xpPoints = defaultXpPoints;
        playerData.xpPointsNeeded = defaultXpPointsNeeded;
    }
}
