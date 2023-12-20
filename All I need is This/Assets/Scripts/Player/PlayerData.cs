using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    // -------- PLAYER DATA -------- \\

    // health + coins
    public float playerHealth = 200f;
    public int coins = 200;

    // xp data
    public int xpLevel = 0;
    public float xpPoints = 0;

    public float xpPointsNeeded = 10f;

    [Header("Exponential Calcs")]
    public float baseNumber = 20f;


    // -------- XP Functions -------- \\

    public void AddXP(float points)
    {
        xpPoints += points;
        Debug.Log($"XP now {xpPoints}");

        if (xpPoints >= xpPointsNeeded)
        {
            xpLevel++;
            xpPoints = 0;
            xpPointsNeeded = CalculateNewRequiredPoints(xpLevel);
            Debug.Log($"New Level Reached, level is now {xpLevel}, points needed are {xpPointsNeeded}");
        }
    }

    // uses the base number and the new level number and an exponential function to determine new points required
    float CalculateNewRequiredPoints(int newLevel)
    {
        return Mathf.Pow(baseNumber, newLevel) * 10;
    }
}
