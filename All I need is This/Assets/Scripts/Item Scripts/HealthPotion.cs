using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    public float healingAmount = 20f;
    public PlayerManager player;

    // when health potion is selected
    public override void Selected()
    {
        Debug.Log("Using health potion");

        player.RestoreHealth(healingAmount);

        Destroy(gameObject);
    }
}
