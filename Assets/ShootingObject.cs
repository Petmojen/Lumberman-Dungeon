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
        Shoot();
    }

    void Shoot()
    {
        if (firedBullet == true)
        {
            Instantiate(bulletPrefab, firePoint);
            Destroy(gameObject, 2);
            firedBullet = false;
        }
    }

}
