using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minDistance; // how close can the enemy get?

    // speed * Time.DeltaTime() --> makes the speed frame independent
    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // attack code, when the enemy is close enough to the player
        }
    }
}
