using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeManager : MonoBehaviour
{
    public int coins = 500;

    public void takeCoins(int amount)
    {
        coins -= amount;
    }

    public void addCoins(int amount)
    {
        coins += amount;
    }
}
