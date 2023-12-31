using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // -------- Variables -------- \\

    // ~ Animation and Rendering ~ \\
    [Header("Animation and Rendering")]
    public SpriteRenderer characterRenderer, weaponRenderer;
    private Sprite swordSprite;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    private bool attackBlocked;

    // ~ Main Sword Class Variables ~ \\

    // general attacking variables
    [Header("Attacking")]
    public float damage = 20f;
    public float delay = 0.3f;
    public float attackRange = 5f;
    public float knockback = 5f;

    // critical hits
    [Header("Critical Hits")]
    [Range(0f, 1f)]
    public float criticalHitChance = 0.2f; // should be 20%
    public float minCritDamage = 30f;
    public float maxCritDamage = 35f;

    // durability
    [Header("Durability")]
    public bool hasLifespan = false;
    public float durability = 0f;

    // shop details
    [Header("Shop Details")]
    public string swordName = "Main Sword";
    public bool owned = true;
    public string category = "One Handed";
    public int cost = 0;
    public int requiredLevel = 0; // decides if sword is locked or grayed-out


    // -------- Setup and Sprite Access -------- \\

    private void Awake()
    {
        swordSprite = weaponRenderer.sprite;
    }

    public Sprite GetSwordSprite()
    {
        return swordSprite;
    }

    // -------- Pointer Updates -------- \\

    private void Update()
    {
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }
    }

    // -------- Attacking Functions -------- \\

    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        attackBlocked = true;
        StartCoroutine(DelayedAttack());
        Debug.Log("Weapon attacking!");

        // get damage to use
        float damageToApply = GetDamage();
        Debug.Log($"Applying {damageToApply} damage");
    }

    private IEnumerator DelayedAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }

    // -------- Applying Damage -------- \\

    // returns either normal damage or a critical damage in the critical range
    public float GetDamage()
    {
        if (CheckForCriticalHit())
        {
            Debug.Log("critical hit!");
            return Random.Range(minCritDamage, maxCritDamage);
        }
        else
        {
            Debug.Log("normal hit");
            return damage;
        }
    }

    public bool CheckForCriticalHit()
    {
        // system picks random number from 0 to 10 inclusive and if this is less than or equal to crit chance variable
        // return true, otherwise false
        if (Random.Range(0f, 1f) <= criticalHitChance)
            return true;
        else
            return false;
    }
}
