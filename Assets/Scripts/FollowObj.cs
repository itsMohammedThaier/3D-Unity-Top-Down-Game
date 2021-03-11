using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public bool withLerp;
    public Transform FollowThis;
    public Transform whoWillMove;
    public float MovingLerp;

    void FixedUpdate()
    {
        FollowTheTarget();
    }
    void FollowTheTarget()
    {
        if (withLerp)
        {
            whoWillMove.position = Vector3.Lerp(whoWillMove.position, FollowThis.position, MovingLerp);
        }
        else
        {
            whoWillMove.position = FollowThis.position;
        }
    }
}
