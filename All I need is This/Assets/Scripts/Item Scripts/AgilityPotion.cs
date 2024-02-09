using System.Collections;
using System.Collections.Generic;
using System.Net;
using TreeEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Agility Potion", menuName = "Item/Agility Potion")]
public class AgilityPotion : Item, IDuration
{
    public float moveSpeedIncrease;

    public PlayerMovement playerMovement;

    public float duration
    {
        get { return 5f; }
    }

    public override void Selected()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        playerMovement.moveSpeed += moveSpeedIncrease;

        // moving the item out of hotbar so that it isn't reused
        // transform.SetParent(player.transform);
    }

    public void DurationEnded()
    {
        playerMovement.moveSpeed -= moveSpeedIncrease;
    }
}
