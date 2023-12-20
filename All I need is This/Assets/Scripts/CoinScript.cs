using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinAmount = 10;
    private bool hasCollided = false;

    public void changeCoinAmount(int newamount)
    {
        coinAmount = newamount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            PlayerManager playerm = collision.gameObject.GetComponent<PlayerManager>();

            playerm.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}
