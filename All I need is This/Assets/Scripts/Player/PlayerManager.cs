using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // CONFIGURABLES
    public float health = 200f; // this is player's max health

    public GameObject healthBar;
    public GameObject safe;

    public void takeDamage(float ammount)
    {
        health -= ammount;

        // update the health bar UI
        Slider healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.value -= ammount;

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

    // setup the health bar (if health value is changed, the health bar needs to change as well)
    private void Start()
    {
        Slider healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
}
