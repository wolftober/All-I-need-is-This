using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnTouch : MonoBehaviour
{
    public WaveManager waveManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("i died");
        waveManager.AnEnemyHasDied();
        Destroy(gameObject);
    }
}
