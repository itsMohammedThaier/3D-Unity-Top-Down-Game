using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool Lerp;
    public float movementLerp;
    public Transform target;
    public Vector3 Offset;
    private Vector3 startPose;

    void Start()
    {
        startPose = transform.position;
    }
    void FixedUpdate()
    {
        FollowObj();
    }

    private void FollowObj()
    {
        Vector3 FinalPose = target.position + Offset;
        if (Lerp)
            transform.position = Vector3.Lerp(transform.position, FinalPose, movementLerp);

        else
            transform.position = FinalPose;
    }
    private void LookAtObject()
    {
        transform.LookAt(target);
    }
}