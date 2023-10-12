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

    // UI
    public GameObject gameOverPanel;

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

    /* 
     * Might need this to reverse the effects of player's death
     * 1. stop time with timescale
     * 2. make the game over panel pop up
     * 3. set the player's gameobject to be unactive
    */
    public void die()
    {
        Debug.Log("Player was killed...");
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameObject.SetActive(false);
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
