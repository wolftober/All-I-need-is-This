using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    private bool hasTakenCoins = false;
    private Stage stage;

    public Transform player; // to get the player's pos
    public GameObject CoinSprite; // to display coins on top of red dude when he steals
    public GameObject coinDropPrefab; // to spawn in the coin drop when red dude

    public GameObject enemyManagerObj;
    public WaveManager waveManager;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] GameObject enPrefab;
    [SerializeField] private bool isDead = false;

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        stage = Stage.Stage_1;
    }
    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;

        health -= amount;

        Debug.Log($"Enemy health: {health}");

        if (health > 0)
        {
            OnHitWithReference?.Invoke(sender);
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
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            die();
        }
    }

    // IMPORTANT : sends message to round manager telling it to check its enemy left count
    public void die()
    {
        // coin drop
        GameObject coinDrop = Instantiate(coinDropPrefab, transform.position, transform.rotation);
        coinDrop.GetComponent<CoinScript>().playerData = player.gameObject.GetComponent<PlayerManager>().playerData;
        Destroy(gameObject);

        // notify wave manager
        waveManager.AnEnemyHasDied();

    }

    private void StartNextStage()
    {
        switch (stage)
        {
            case Stage.Stage_1:
                stage = Stage.Stage_2;
                spawnEnemy();
                spawnEnemy();

                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3; 
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();

                break;
            case Stage.Stage_3:
                stage = Stage.Stage_4;
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();
                spawnEnemy();

                break;
        }

        Debug.Log("Starting next stage " + stage);
    }

    private void spawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enPrefab, new Vector3(this.transform.position.x + Random.Range(-4f, 4), this.transform.position.y + Random.Range(4f, 4), 0), Quaternion.identity);
        EnemyScript enemyScript = spawnedEnemy.GetComponent<EnemyScript>();
        enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;

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
            Debug.Log("Hit you");
        }
    }
}