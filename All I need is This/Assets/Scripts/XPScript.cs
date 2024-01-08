using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class XPScript : MonoBehaviour
{
    public int xpAmount = 10;
    private bool hasCollided = false;

    public PlayerData playerData;

    [HideInInspector]
    public UnityEvent xpCollected;

    // UI Updates
    private XPBar xpBar;

    public void ChangeXPAmount(int newAmount)
    {
        xpAmount = newAmount;
    }

    void Start()
    {
        xpBar = GameObject.FindGameObjectWithTag("XP Bar").GetComponent<XPBar>();

        // adding listeners
        xpCollected.AddListener(xpBar.UpdateXPBar);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;

            playerData.AddXP(xpAmount);

            xpCollected.Invoke();

            Destroy(gameObject);
        }
    }
}
