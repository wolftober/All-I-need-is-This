using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // CONFIGURABLES
    public float health = 200f; // this is player's max health
    public int coins = 200;

    public GameObject healthBar;
    public GameObject safe;
    private Vector2 PointerInput;

    private Sword weaponParent;
    // private FaceMouse pointerPos;
    // UI
    public GameObject Canvas;
    public UIManager UIManager;

    private void Awake()
    {
        weaponParent = GetComponentInChildren<Sword>();
        // pointerPos = GetComponent<FaceMouse>();
    }

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

        // UI Manager Call
        UIManager.OpenGameOverMenu();

        gameObject.SetActive(false);
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        // tell UI manager to update the coin display
        UIManager.UpdateCoinCount(coins);
    }

    public void TakeAwayCoins(int amount)
    {
        coins -= amount;

        UIManager.UpdateCoinCount(coins);
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

    private void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PerformAttack();
        PointerInput = GetPointerInput();
        weaponParent.PointerPosition = PointerInput;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
