using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;
    public Transform target; // the player
    public float moveSpeed = 2f;

    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPos = target.position;
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }
}
