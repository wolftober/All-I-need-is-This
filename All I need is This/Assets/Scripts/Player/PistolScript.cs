using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PistolScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public int magazineSize = 6;
    public int currentAmmo = 6;
    public float reloadTime; // will be implemented later

    public GameObject ammoText;

    void Update()
    {
        // When left mouse button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // will reload after player uses last boolet to keep sanity
            if (currentAmmo > 1)
            {
                currentAmmo--;
                updateUI();
                Shoot();
            }
            else if (currentAmmo == 1)
            {
                // shoot and reload
                currentAmmo--;
                updateUI();
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
        updateUI();
    }

    private void Start()
    {
        updateUI();
    }

    void updateUI()
    {
        TextMeshProUGUI txt = ammoText.GetComponent<TMPro.TextMeshProUGUI>();
        txt.text = "Ammo : " + currentAmmo + "/" + magazineSize;
    }
}
