using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;

    public Transform player; // to get the player's pos
    public Transform safe; // to get the safe's pos

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

    void Update()
    {
        Vector3 targetPos = player.position;

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager playermanager = collision.GetComponent<PlayerManager>();
        if (playermanager != null)
        {
            playermanager.takeDamage(damage);
        }
    }
}
