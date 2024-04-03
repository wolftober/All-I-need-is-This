using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;

    // CONFIGURABLES
    //public float health = 200f; // this is player's max health
    private float maxHealth;
    public int coins = 200;

    public GameObject safe;
    private Vector2 PointerInput;

    [HideInInspector]
    public Sword weaponParent;
    // private FaceMouse pointerPos;
    // UI
    public GameObject Canvas;
    public UIManager uiManager;

    public Slider healthSlider;

    private void Awake()
    {
        weaponParent = GetComponentInChildren<Sword>();
        // pointerPos = GetComponent<FaceMouse>();
    }

    public void RestoreHealth(float amount)
    {
        if (playerData.playerHealth < maxHealth)
        {
            float newHealth = playerData.playerHealth + amount;
            if (newHealth > maxHealth) { newHealth = maxHealth; }

            playerData.playerHealth = newHealth;
        }
    }

    public void takeDamage(float amount)
    {
        playerData.playerHealth -= amount;
        Debug.Log($"Took damage, health is now {playerData.playerHealth}");

        if (playerData.playerHealth <= 0)
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
        uiManager.OpenGameOverMenu();

        gameObject.SetActive(false);
    }

    public void AddCoins(int amount)
    {
        playerData.coins += amount;
    }

    public void TakeAwayCoins(int amount)
    {
        playerData.coins -= amount;
    }

    /*
    public void returnToSafe(int coins)
    {
        SafeManager safem = safe.GetComponent<SafeManager>();
        safem.addCoins(coins);
    }
    */

    // setup the health bar (if health value is changed, the health bar needs to change as well)
    private void Start()
    {
        maxHealth = playerData.playerHealth;
    }

    private void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && uiManager.openedWindows.Count == 0)
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
