
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float fieldOfImpact;
    public float forcePower;

    void Start()
    {
        Exsplode();
    }


    void Exsplode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, fieldOfImpact);

        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = hit.transform.position - transform.position;
                rb.AddForce(direction * forcePower);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, fieldOfImpact);
    }
}
