using UnityEngine;

public class ArrowCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        transform.parent = coll.transform;
        Destroy(this);
    }
}
