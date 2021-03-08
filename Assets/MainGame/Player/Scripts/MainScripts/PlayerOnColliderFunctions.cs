using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnColliderFunctions : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement movement;
    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.tag == "Ground")
        {
            movement.ResetJumpTimes();
        }
    }
}
