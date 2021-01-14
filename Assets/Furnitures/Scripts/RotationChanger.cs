using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChanger : MonoBehaviour
{
    public Transform whoWillRotate;
    public bool ActiveTheRotationMovement;
    public float xRotatingSpeed;
    public float yRotatingSpeed;
    public float zRotatingSpeed;
    public Vector3 test;
    private Vector3 nextRotate;
    void Start()
    {
        whoWillRotate = (whoWillRotate != null)? whoWillRotate: transform;
        nextRotate = whoWillRotate.rotation.eulerAngles;
    }

    void Update()
    {
        if (ActiveTheRotationMovement)
        {
            RotateTheObject();
        }
    }
    public void RotateTheObject()
    {
        nextRotate += new Vector3(xRotatingSpeed, yRotatingSpeed, zRotatingSpeed);
        Quaternion nextFrameRotate = Quaternion.Euler(nextRotate);
        whoWillRotate.rotation = nextFrameRotate;
        if (whoWillRotate.rotation == Quaternion.Euler(Vector3.zero))
        {
            ResetRotation();
        }

    }
    private void ResetRotation()
    {
        whoWillRotate.rotation = Quaternion.Euler(Vector3.zero);
    }
}
