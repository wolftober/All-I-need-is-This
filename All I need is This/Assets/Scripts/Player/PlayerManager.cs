using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // CONFIGURABLES
    public float health = 200f;

    public GameObject safe;

    public void takeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("Player was killed...");
    }

    public void returnToSafe(int coins)
    {
        SafeManager safem = safe.GetComponent<SafeManager>();
        safem.addCoins(coins);
    }
}
