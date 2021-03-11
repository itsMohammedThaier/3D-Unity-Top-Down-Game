using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTheMouse : MonoBehaviour
{
    private Vector3 pointToLookAt;
    public Transform player;
    void Update()
    {
        LookAtMouse();
    }
    public void LookAtMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, player.position.y, 0));
        float rayLength = 200;
        if (groundPlane.Raycast(camRay, out rayLength))
        {
            pointToLookAt = camRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLookAt.x, pointToLookAt.y, pointToLookAt.z));
        }
    }
}
