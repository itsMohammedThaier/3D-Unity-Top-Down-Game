using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleGunsFire : MonoBehaviour
{
    public GameObject bullet, fireEffect;
    public Transform[] firePoint;
    public Animator[] fireAnimations;

    public float bulletSpeed;
    public float shootTimer;
    private float timeWhenAllowedNextShoot = 0f;
    private int currentGunIndex, lastGunIndex;

    void Start()
    {
        currentGunIndex = 0; lastGunIndex = firePoint.Length - 1;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && timeWhenAllowedNextShoot <= Time.time)
        {
            StartFire();
        }
    }

    void StartFire()
    {
        ChangeSelectedGun();
        SpawnBullet();
        PlayFireEffects();
        timeWhenAllowedNextShoot = Time.time + shootTimer;
    }
    void ChangeSelectedGun()
    {
        if (currentGunIndex >= lastGunIndex)
        {
            currentGunIndex = 0;
            return;
        }
        currentGunIndex++;
    }
    void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet, firePoint[currentGunIndex].transform.position, firePoint[currentGunIndex].transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = firePoint[currentGunIndex].forward * bulletSpeed;
    }
    void PlayFireEffects()
    {
        fireAnimations[currentGunIndex].Play("Fire");
        Instantiate(fireEffect, firePoint[currentGunIndex].transform.position, firePoint[currentGunIndex].transform.rotation);
    }
}
