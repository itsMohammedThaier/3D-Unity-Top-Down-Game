using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet, fireEffect;
    public Transform firePoint;
    public float BulletSpeed;
    public float FireDuration;
    private float currentFireDuration;

    void Update()
    {
        if (currentFireDuration < Time.time)
            Shoot();
    }

    void Shoot()
    {
        InstaniateBullet();
        PlayEffects();

        currentFireDuration = Time.time + FireDuration;
    }
    void InstaniateBullet()
    {
        GameObject instantiatedBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        instantiatedBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * BulletSpeed;
    }
    void PlayEffects()
    {
        Instantiate(fireEffect, firePoint.position, firePoint.rotation);
    }
}
