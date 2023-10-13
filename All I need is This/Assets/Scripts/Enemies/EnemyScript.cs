using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // customizables
    public float health = 100f;
    public float maxHealth = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public int takeAmount = 10;

    private bool hasTakenCoins = false;

    public Transform player; // to get the player's pos
    public Transform safe; // to get the safe's pos
    public GameObject safeObj; // to return money to safe if red dude is caught
    public GameObject CoinSprite; // to display coins on top of red dude when he steals
    public GameObject coinDropPrefab; // to spawn in the coin drop when red dude

    public GameObject enemyManagerObj;
    [SerializeField] EnemyHealthBar healthBar;

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake() 
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            die();
        }
    }

    // IMPORTANT : sends message to round manager telling it to check its enemy left count
    public void die()
    {
        RoundManager rm = enemyManagerObj.GetComponent<RoundManager>();

        // if red dude dies while he has taken coins, those coins will be dropped on the ground
        // for the player to pick them up
        if (hasTakenCoins == true)
        {
            // spawns the coindrop prefab into the scene at the red dude's position
            Instantiate(coinDropPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            rm.checkEnemyCount();
        }
        else
        {
            Destroy(gameObject);
            rm.checkEnemyCount();
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

            // calls the enemy manager object script to obtain a destination position
            BaseManager basem = enemyManagerObj.GetComponent<BaseManager>();
            Vector3 targetPosition = basem.getTargetPosition(transform.position);

            if (transform.position == targetPosition)
            {
                // this occurs when enemy reaches the base
                die();
            }

            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
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
