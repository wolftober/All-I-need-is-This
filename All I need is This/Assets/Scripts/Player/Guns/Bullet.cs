using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 22f;
    public float damage = 10f;

    public Rigidbody2D rb;

    void Start()
    {
        // as soon as bullet spawns, we tell it to fly forward
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        // access an enemy's script's function that takes damage:
        // <Script Name> name = hitinfo.GetComponent<<Script Name>>();
        // if (name != null) { name.takedamage(dmg) }

        if (hitinfo.CompareTag("Enemy"))
        {
            EnemyScript enemyscript = hitinfo.GetComponent<EnemyScript>();
            if (enemyscript != null)
            {
                // enemyscript.GetHit(damage);
                return;
            }

            if (!hitinfo.CompareTag("Coins"))
            {
                Destroy(gameObject);
            }
        }
        else if (hitinfo.CompareTag("Boss"))
        {
            BossScript bossscript = hitinfo.GetComponent<BossScript>();
            if (bossscript != null)
            {
                //bossscript.GetHit(damage);
                return;
            }

            if (!hitinfo.CompareTag("Coins"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
