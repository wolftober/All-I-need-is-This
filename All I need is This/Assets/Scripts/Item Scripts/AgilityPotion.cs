using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class AgilityPotion : Item
{
    public float moveSpeedIncrease;
    public float duration;

    public PlayerMovement player;

    public override void Selected()
    {
        player.moveSpeed += moveSpeedIncrease;

        // moving the item out of hotbar so that it isn't reused
        transform.parent = player.transform;

        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(duration);
        player.moveSpeed -= moveSpeedIncrease;
        Debug.Log("Back to normal");

        Destroy(gameObject);
    }
}
