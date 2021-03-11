using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public ParticleSystem effectParticles;
    private float distance;
    private Vector3 startPos, hitPos;

    void Start()
    {
        PrepareTheRay();
        startPos = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= distance)
        {
            Hit();
        }
    }

    public void PrepareTheRay()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            distance = Vector3.Distance(hit.point, transform.position);
            hitPos = hit.point;
            return;
        }
        distance = 1000000;
    }


    void Hit()
    {
        transform.position = hitPos;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        effectParticles.Stop();
    }
}
