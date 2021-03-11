using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject arrow;
    public Transform firePoint;
    private Animator arrowAnimator;
    public float bowSpeed;
    private bool ReadyToShoot;
    public void SetReadyToShoot()
    {
        ReadyToShoot = true;
    }
    public void SetNotReadyToShoot()
    {
        ReadyToShoot = false;
    }

    void Start()
    {
        ReadyToShoot = false;
        arrowAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        PlayerInput();
    }
    private void PlayerInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayAnimation("PrepareToShoot");
            return;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (ReadyToShoot)
            {
                PlayAnimation("Shoot");
            }
            else
            {
                PlayAnimation("Default");
            }
            return;
        }
    }

    private void Shoot()
    {
        InstantiateArrow();
        PlayAnimation("Default");
    }
    private void InstantiateArrow()
    {
        GameObject instantiatedBow = Instantiate(arrow, firePoint.position, firePoint.rotation);
        instantiatedBow.GetComponent<Rigidbody>().velocity = firePoint.forward * bowSpeed;
    }

    private void PlayAnimation(string x)
    {
        arrowAnimator.Play(x);
    }
}