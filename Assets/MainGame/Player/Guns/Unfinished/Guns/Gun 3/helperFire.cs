using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helperFire : MonoBehaviour
{
    public static Vector3 pointToLookAt;
    public Transform player;

    public GameObject bullet;
    public Transform firePoint;


    public float bulletSpeed;
    public float shootTimer;
    private float timeWhenAllowedNextShoot = 0f;

    void Update()
    {
        FaceTheMouse();
        if (Input.GetButton("Fire1") && timeWhenAllowedNextShoot <= Time.time)
        {
            StartFire();
        }
    }
    public void FaceTheMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, player.position.y, 0));
        float rayLength = 200;
        if (groundPlane.Raycast(camRay, out rayLength))
        {
            pointToLookAt = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLookAt.x, pointToLookAt.y, pointToLookAt.z));
        }
    }


    void StartFire()
    {
        SpawnBullet();


        timeWhenAllowedNextShoot = Time.time + shootTimer;
    }
    void SpawnBullet()
    {
        GameObject spawnedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
    }
}
