using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public float fireRate = 2f;
    public GameObject bulletPrefab;

    private float _shootTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _shootTime)
        {
            _shootTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
