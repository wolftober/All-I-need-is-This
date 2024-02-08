using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    // customizables
    public float health = 100f;
    public float maxHealth = 100f;
    public float moveSpeed = 2f;
    public float damage = 10f;
    public int takeAmount = 10;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    public Transform player; // to get the player's pos
    public GameObject CoinSprite; // to display coins on top of red dude when he steals
    public GameObject coinDropPrefab; // to spawn in the coin drop when red dude

    public WaveManager waveManager;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] private bool isDead = false;

    private void Start()
    {
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake() 
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void GetHit(int amount, GameObject sender)
    {
        if (isDead)
            return;

        health -= amount;

        Debug.Log($"Enemy health: {health}");

        if(health > 0)
        {
            OnHitWithReference?.Invoke(sender);
            healthBar.UpdateHealthBar(health, maxHealth);
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
    }
}
