using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // customizables
    public float health = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public int takeAmount = 10;

    private bool hasTakenCoins = false;

    public Transform player; // to get the player's pos
    public Transform safe; // to get the safe's pos
    public GameObject safeObj; // to return money to safe if red dude is caught
    public GameObject CoinSprite; // to display coins on top of red dude when he steals
    public GameObject coinDropPrefab;

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
        // if red dude dies while he has taken coins, those coins will be dropped on the ground
        // for the player to pick them up
        if (hasTakenCoins == true)
        {
            // spawns the coindrop prefab into the scene at the red dude's position
            Instantiate(coinDropPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // MOVEMENT

        // if red dude hasn't taken coins yet, he will chase something
        // if he has taken something, he will run away to some base

        if (hasTakenCoins == false)
        {
            // red dude will default to chasing the player unless the safe is closer
            Vector3 targetPos = player.position;

            float distance_to_player = Vector3.Distance(transform.position, player.position);
            float distance_to_safe = Vector3.Distance(transform.position, safe.position);

            if (distance_to_safe < distance_to_player)
            {
                targetPos = safe.position;
            }

            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        else
        {
            // red dude will try to go back to base where he will despawn
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // COLLISIONS

        // if the collision is with a player
        PlayerManager playermanager = collision.GetComponent<PlayerManager>();
        if (playermanager != null)
        {
            playermanager.takeDamage(damage);
        }

        // if the collision is with the safe
        SafeManager safemanager = collision.GetComponent<SafeManager>();
        if (safemanager != null && hasTakenCoins == false)
        {
            hasTakenCoins = true;
            CoinSprite.SetActive(true);
            safemanager.takeCoins(takeAmount);
        }
    }
}
