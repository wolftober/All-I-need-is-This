using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinScript : MonoBehaviour
{
    public int coinAmount = 10;
    private bool hasCollided = false;

    public PlayerData playerData;

    [HideInInspector]
    public UnityEvent coinCollected;

    // for UI updates, variables are set with GameObject tag references
    UIManager uiManager;
    ShopManager shopManager;

    public void changeCoinAmount(int newAmount)
    {
        coinAmount = newAmount;
    }

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("Player UI").GetComponent<UIManager>();
        shopManager = GameObject.FindGameObjectWithTag("Shop Manager").GetComponent<ShopManager>();

        // adding listeners to event
        coinCollected.AddListener(uiManager.UpdateCoinCount);
        coinCollected.AddListener(shopManager.UpdateCoinCount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;

            // adding coins to player data
            playerData.AddCoins(coinAmount);

            // telling UI elements to update themselves
            coinCollected.Invoke();

            Destroy(gameObject);
        }
    }
}
