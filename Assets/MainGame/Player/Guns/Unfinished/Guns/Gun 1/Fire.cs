using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bullet, fireEffect;
    public Transform firePoint;
    public Animator fireAnimation;

    public float bulletSpeed;
    public float shootTimer;
    private float timeWhenAllowedNextShoot = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && timeWhenAllowedNextShoot <= Time.time)
        {
            StartFire();
        }
    }

    void StartFire()
    {
        SpawnBullet();
        PlayFireEffects();


        timeWhenAllowedNextShoot = Time.time + shootTimer;
    }
    void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
    }
    void PlayFireEffects()
    {
        fireAnimation.Play("Fire");
        Instantiate(fireEffect, firePoint.transform.position, firePoint.transform.rotation);
    }
}
