using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    void Update()
    {
        // When left mouse button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
