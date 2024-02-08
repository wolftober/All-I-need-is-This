using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    // customizables
    public enum Stage
    {
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
    }

    public float health = 100f;
    public float maxHealth = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public int takeAmount = 10;

    private bool hasTakenCoins = false;
    private Stage stage;

    public Transform player; // to get the player's pos
    /// public Transform safe; // to get the safe's pos
    /// public GameObject safeObj; // to return money to safe if red dude is caught
    public GameObject CoinSprite; // to display coins on top of red dude when he steals
    public GameObject coinDropPrefab; // to spawn in the coin drop when red dude

    public GameObject enemyManagerObj;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] GameObject enPrefab;

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        stage = Stage.Stage_1;
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);

        switch (stage)
        {
            case Stage.Stage_1:
                if (health < maxHealth * .75)
                {
                    // bellow 75%
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:
                if (health < maxHealth * .50)
                {
                    // bellow 50%
                    StartNextStage();
                }
                break;
            case Stage.Stage_3:
                if (health < maxHealth * .25)
                {
                    // bellow 25%
                    StartNextStage();
                }
                break;
        }

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

    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                break;
            case Stage.Stage_3:
                stage = Stage.Stage_4;
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                Instantiate(enPrefab, new Vector3(Random.Range(-5f, 5), Random.Range(6f, 6), 0), Quaternion.identity);
                break;
        }

        Debug.Log("Starting next stage " + stage);
    }

    void Update()
    {
        // MOVEMENT

        // red dude will default to chasing the player
        Vector3 targetPos = player.position;

        float distance_to_player = Vector3.Distance(transform.position, player.position);

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

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