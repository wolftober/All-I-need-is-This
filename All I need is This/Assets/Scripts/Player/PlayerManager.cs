using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health = 200f;

    public void takeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("Player was killed...");
    }
}
