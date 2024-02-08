using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public LeversManager leversManager;
    void Start()
    {
        leversManager.Subscribe();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        leversManager.LeverActivated();
        Destroy(gameObject);
    }
}
