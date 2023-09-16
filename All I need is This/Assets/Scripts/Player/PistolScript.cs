using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public int magazineSize = 6;
    public int currentAmmo = 6;

    void Update()
    {
        // When left mouse button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // will reload after player uses last boolet to keep sanity
            if (currentAmmo > 1)
            {
                currentAmmo--;
                Shoot();
            }
            else if (currentAmmo == 1)
            {
                // shoot and reload
                currentAmmo--;
                Shoot();
                Reload();
            }
        }

        // when reload button is pressed (like R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }

    void Reload()
    {
        Debug.Log("instant reload...");
        currentAmmo = magazineSize;
    }
}
