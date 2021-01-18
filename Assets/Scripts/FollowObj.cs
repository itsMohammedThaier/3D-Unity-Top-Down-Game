using UnityEngine;

public class FollowObj : MonoBehaviour
{
    public bool withLerp;
    public Transform FollowThis;
    public Transform whoWillMove;
    public float MovingLerp;

    void Update()
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
