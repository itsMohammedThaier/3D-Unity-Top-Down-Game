using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody whoWillMove; 

    public float movingSpeed;
    public float jumpForce;
    public float gravity; /* This should return positive number 
    cause we'll return it to negative later*/

    public float maximizeZAxisSpeed;/* The maximize speed you can reach when you use  normal speed */
    public float maximizeZAxisSuperSpeed;/*The maximize speed you can reach when you use the super speed */
    public float maximizeXAxisSpeed;
    private float selectedZAxisSpeed;
    private float selectedXAxisSpeed;

    private Vector3 forwardMove;
    private Vector3 sideMove;

    public ParticleSystem boostUpParticles;

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
        InputJumpButtons();
    }

    public void Input_Movement_Horizontal_Keys()
    {
        if ( Input.GetButton("Horizontal") )
        {
            MoveRightOrLeft();
        }
        else if ( !Input.GetButton("Horizontal") )
        {
            MoveRightOrLeft();
        }
    }
    public void MoveRightOrLeft()
    {
        sideMove = whoWillMove.transform.right*selectedXAxisSpeed*Input.GetAxis("Horizontal");
        Move();
    }


    public void Input_Movement_Vertical_Keys()
    {
        if( Input.GetButton("Vertical"))
        {
            MoveForwardOrTowrward();
        }
        else if ( !Input.GetButton("Vertical") )
        {
            forwardMove = Vector3.zero;
        }
    }
    public void MoveForwardOrTowrward()
    {
        forwardMove = whoWillMove.transform.forward*selectedZAxisSpeed*Input.GetAxis("Vertical");
        Move();
    }
    public void Move()
    {
        Vector3 addedMovement = sideMove + forwardMove;
        whoWillMove.velocity = new Vector3(addedMovement.x, whoWillMove.velocity.y, addedMovement.z);
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
        Vector3 ScaleWithBoost = new Vector3(.7f, .7f, 2);
        Vector3 ScaleWitoutBoost = new Vector3(1f, 1f, 1);
        if (IncreaseTheSpeed)
        {
            selectedZAxisSpeed = maximizeZAxisSuperSpeed;
            whoWillMove.transform.localScale = Vector3.Lerp(whoWillMove.transform.localScale, ScaleWithBoost, .02f);
            TurnTurboParticles(true);
        }
        else
        {
            selectedZAxisSpeed = maximizeZAxisSpeed;
            whoWillMove.transform.localScale = Vector3.Lerp(whoWillMove.transform.localScale, ScaleWitoutBoost, .1f);
            TurnTurboParticles(false);
        }
    }
    public void TurnTurboParticles(bool PlayParticles)
    {
        if (PlayParticles)
        {
            boostUpParticles.Play();
        }
        else
        {
            if(boostUpParticles.isPlaying)
            {
                boostUpParticles.Stop();
            }
        }
    }


    public void InputJumpButtons()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
        IncreaGravity();
    }
    public void jump()
    {
        whoWillMove.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
    public void IncreaGravity()
    {
        whoWillMove.AddForce(0, -gravity, 0);
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