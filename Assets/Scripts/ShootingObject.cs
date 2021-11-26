using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObject : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    bool firedBullet = true;

    void Update()
    {
        if (firedBullet)
        {
            InvokeRepeating(nameof(Shoot), 0, 1);
            firedBullet = false;
        }
    }

    void Shoot()
    {

        GameObject bulletinstance = Instantiate(bulletPrefab, firePoint);
        Destroy(bulletinstance, 2);
    }

}
