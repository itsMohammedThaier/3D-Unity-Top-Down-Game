using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePlayer : MonoBehaviour
{
    public bool followPosition;
    public bool followRotation;
    public Transform player;
    void Update()
    {
        if (followPosition)
        {
            transform.position = player.position;
        }
        if (followRotation)
        {
            transform.rotation = player.rotation;
        }
    }
}
