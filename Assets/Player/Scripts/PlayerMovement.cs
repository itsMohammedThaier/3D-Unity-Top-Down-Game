using UnityEngine;
using System;  

public class PlayerMovement : MonoBehaviour
{
    //TODO: reset the operators places
    public Rigidbody whoWillMove; 

    public float movingSpeed;
    public float jumpForce;
    public float gravity; /* This will return positive number cause we'll return it to negative later*/
    public GameObject jumpParticles;
    public Transform instantiatedJumpParticlesPos;
    public float defaultJumpTimes;
    private float currentJumpTimes;

    public GameObject wings;

    public float maximizeZAxisSpeed;/* The maximize speed you can reach when you use  normal speed */
    public float maximizeZAxisSuperSpeed;/* The maximize speed you can reach when you use the super speed */
    public float maximizeXAxisSpeed;
    private float selectedZAxisSpeed;
    private float selectedXAxisSpeed;

    private Vector3 forwardMove;
    private Vector3 sideMove;

    public ParticleSystem boostUpParticles;

    public float facingTheMouseLerp;
    private Vector3 pointToLookAt;

    private string movementMode; //Move modes: 1.Normal Mode 2.Flying Mmode
    public string MovementMode
    {
        get
        {
            return movementMode;
        }
        set
        {
            if (value == "Fly Mode" || value == "Normal Mode")
            {
                movementMode = value;
            }
            else
            {
                Debug.Log("You can't change the MovementMode name to something else than Fly Mode or Normal Mode");
                movementMode = "Normal Mode";
            }
        }
    }

    void Start()
    {
        MovementMode = "Normal Mode";
        selectedZAxisSpeed = maximizeZAxisSpeed;
        selectedXAxisSpeed = maximizeXAxisSpeed;
        ResetJumpTimes();
    }
    void Update()
    {
        MovementKeyboardInput(MovementMode);
        FaceTheMouse();
    }

    public void MovementKeyboardInput(string MovingInputMode)
    {
        Input_Movement_Horizontal_Keys();
        Input_Movement_Vertical_Keys();
        InputBoosButtons();
        switch (MovingInputMode)
        {
            case "Normal Mode": InputJumpButtons(); break;

            case "Fly Mode": PlayFlyModeActions(); break;
            
            default:
            //TODO: make the default case
            break;
        }
        if (MovingInputMode == "Normal Mode")
        {
            InputJumpButtons();
        }
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
            //If the allowed jump times become zero, Then
            //The player will active the wings
            if (currentJumpTimes <= 0)
            {
                if (movementMode != "Fly Mode")
                {
                    SetMovementMode("Fly Mode");
                }
            }
            else
            {
                Jump();
                currentJumpTimes--;
            }
        }
        IncreaGravity();
    }
    public void Jump()
    {
        whoWillMove.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        PlayJumpEffects();
        //TODO: Limit the Jump times and enable fly mode
    }
    public void PlayJumpEffects()
    {
        Instantiate(jumpParticles, instantiatedJumpParticlesPos);
    }
    public void ResetJumpTimes()
    {
        currentJumpTimes = defaultJumpTimes;
    }
    public void IncreaGravity()
    {
        whoWillMove.AddForce(0, -gravity, 0);
    }

    public void SetMovementMode(string Mode)
    {
        MovementMode = Mode;
    }
    public void PlayFlyModeActions()
    {
        IncreaGravity();
        ChangeWingsScale();
        //TODO: Make the flying function
    }
    public void ChangeWingsScale()
    {
        wings.transform.localScale = Vector3.Lerp(wings.transform.localScale, new Vector3(1, 1, 1), .125f);
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