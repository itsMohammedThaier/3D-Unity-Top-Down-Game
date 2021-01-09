using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody whoWillMove; 

    public float movingSpeed;

    public float maximizeZAxisSpeed;/* The maximize speed you can reach when you use  normal speed */
    public float maximizeZAxisSuperSpeed;/*The maximize speed you can reach when you use the super speed */
    public float maximizeXAxisSpeed;
    private float selectedZAxisSpeed;
    private float selectedXAxisSpeed;

    public float facingTheMouseLerp;
    private Vector3 pointToLookAt;

    void Start()
    {
        selectedZAxisSpeed = maximizeZAxisSpeed;
        selectedXAxisSpeed = maximizeXAxisSpeed;
    }

    void Update()
    {
        MovementKeyboardInput();
        FaceTheMouse();
    }

    public void MovementKeyboardInput()
    {
        Input_Movement_Horizontal_Keys();
        Input_Movement_Vertical_Keys();
        InputBoosButtons();
    }

    public void Input_Movement_Horizontal_Keys()
    {
        if ( Input.GetButton("Horizontal") )
        {
            MoveRightOrLeft();
        }
    }
    public void MoveRightOrLeft()
    {
        whoWillMove.velocity = whoWillMove.transform.right*selectedXAxisSpeed*Input.GetAxis("Horizontal");
    }


    public void Input_Movement_Vertical_Keys()
    {
        if( Input.GetButton("Vertical"))
        {
            MoveForwardOrTowrward();
        }
    }
    public void MoveForwardOrTowrward()
    {
        whoWillMove.velocity = whoWillMove.transform.forward*selectedZAxisSpeed*Input.GetAxis("Vertical");
    }

    public void InputBoosButtons()
    {
        if (Input.GetButton("SpeedUp"))
        {
            SpeedUp(true);
        }
        else
        {
            SpeedUp(false);
        }
    }
    public void SpeedUp(bool IncreaseTheSpeed)
    {
        if (IncreaseTheSpeed)
        {
            selectedZAxisSpeed = maximizeZAxisSuperSpeed;
        }
        else
        {
            selectedZAxisSpeed = maximizeZAxisSpeed;
        }
    }


    public void FaceTheMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane (Vector3.up, new Vector3(0, 0, 0));
        float rayLength = 200;

        if (groundPlane.Raycast(camRay, out rayLength))
        {
            pointToLookAt = Vector3.Lerp(
                pointToLookAt,
                camRay.GetPoint(rayLength),
                facingTheMouseLerp
                );
            whoWillMove.transform.LookAt(new Vector3(pointToLookAt.x, whoWillMove.transform.position.y, pointToLookAt.z));
            Debug.DrawLine(camRay.origin, pointToLookAt, Color.green);
        }
    }
}