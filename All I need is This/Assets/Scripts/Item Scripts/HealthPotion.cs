using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Item/Health Potion")]
public class HealthPotion : Item
{
    public float healingAmount = 20f;
    //public PlayerManager player;

    // when health potion is selected
    public override void Selected()
    {
        Debug.Log("Using health potion");

        player.RestoreHealth(healingAmount);
    }
}
