using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    private Transform bulletPoint;
    [SerializeField]
    private float bulletSpeed = 10f;
    public float damage = 3f;

    private void Awake()
    {
        bulletPoint = transform.Find("Bulletpoint");
        damage = 3f;
    }

    public void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        bullet.transform.parent = gameObject.transform;
        bullet.GetComponent<Rigidbody>().velocity = bulletPoint.forward * bulletSpeed;
    }
}
