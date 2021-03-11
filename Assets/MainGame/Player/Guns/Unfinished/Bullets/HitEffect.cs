using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject Effect;
    public float effectLiveDuration;
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
            PlayHitEffect();
            Destroy(gameObject);
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
    

    void PlayHitEffect()
    {
        GameObject instantiatedEffect = Instantiate(Effect, hitPos, transform.rotation);
        Destroy(instantiatedEffect, effectLiveDuration);
    }
}
