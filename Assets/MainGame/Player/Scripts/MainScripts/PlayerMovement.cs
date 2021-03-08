using UnityEngine;
using System;  

public class PlayerMovement : MonoBehaviour
{
    //TODO: reset the operators places
    public Rigidbody whoWillMove; 
    
    public float movingSpeed;
    
    public float maximizeZAxisSpeed;/* The maximize speed you can reach when you use  normal speed */
    public float maximizeZAxisSuperSpeed;/* The maximize speed you can reach when you use the super speed */
    public float maximizeXAxisSpeed;
    private float selectedZAxisSpeed;
    private float selectedXAxisSpeed;
    private Vector3 forwardMove;
    private Vector3 sideMove;
    public ParticleSystem boostUpParticles;

    public float jumpForce;
    public float gravity; /* This will return positive number cause we'll return it to negative later*/
    public GameObject jumpParticles;
    public Transform instantiatedJumpParticlesPos;
    public float defaultJumpTimes;
    private float currentJumpTimes;

    public GameObject wings;
    public Transform wingUpperBone;
    public Animator flyAnimation;
    private float flyingHigh;
    
    public float facingTheMouseLerp;
    private Vector3 pointToLookAt;

    public float dashDistance;
    public GameObject dashEffect;

    private string movementMode; //Move modes: 1.Normal Mode 2.Flying Mmode
    public string MovementMode
    {
        get{ return movementMode; }
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
        InputDashKeys();
        switch (MovingInputMode)
        {
            case "Normal Mode": InputJumpButtons(); break;

            case "Fly Mode": PlayFlyModeActions(); break;
            
            default:
            //TODO: make the default case
            break;
        }
    }

    public void Input_Movement_Horizontal_Keys()
    {
        if ( Input.GetButton("Horizontal") )
        {
            MoveRightOrLeft();
        }
        else if( !Input.GetButton("Horizontal") )
        {
            sideMove = Vector3.zero;
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
        bool increaseTheSpeed;
        increaseTheSpeed = (Input.GetButton("SpeedUp"))? true:false;
        SpeedUp(increaseTheSpeed);
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
            if (boostUpParticles.isPlaying)
            {
                boostUpParticles.Stop();
            }
        }
    }


    public void InputJumpButtons()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //If the allowed jump times == zero, Then
            //The player press jump, this will active the wings
            if (currentJumpTimes <= 0 && movementMode != "Fly Mode")
            {
                SetMovementMode("Fly Mode");
            }
            else
            {
                Jump();
                currentJumpTimes--;
            }
        }

        /*We can't change the gravity directly from the
        Rigidbody component, So we'll do it via function */
        IncreaGravity();
    }
    public void Jump()
    {
        whoWillMove.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        PlayJumpEffects();
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
        if (movementMode == "Fly Mode")
        {
            EnableFlyMode(true);
        }
    }
    public void EnableFlyMode(bool Enable)
    {
        if (Enable)
        {
            Debug.Log("The fly enabled");
            wings.active = true;
            flyAnimation.SetTrigger("Fly On");
            flyingHigh = whoWillMove.transform.position.y;
        }
        else
        {
            Debug.Log("The fly disabled");
            wings.active = false;
            flyAnimation.SetTrigger("Fly Off");
            MovementMode = "Normal Mode";
            whoWillMove.velocity = new Vector3(whoWillMove.velocity.x, 0, whoWillMove.velocity.z);
        }
    }
    
    public void PlayFlyModeActions()
    {
        bool theWingsShouldGetBigger = CheckFlyButtonIsPressed();
        ChangeWingsScale(theWingsShouldGetBigger);
        Fly();
    }
    public bool CheckFlyButtonIsPressed()
    {
        bool valueToReturn = (Input.GetButton("Jump"))? true:false ;
        return valueToReturn;
    }
    public void ChangeWingsScale(bool toBigger)
    {
        if (toBigger)
        {
            wings.transform.localScale = Vector3.Lerp(wings.transform.localScale, new Vector3(1, 1, 1), .125f);
        }
        else
        {
            wings.transform.localScale = Vector3.Lerp(wings.transform.localScale, new Vector3(0, 1, 0), .125f);
            if (wings.transform.localScale.x < .2f)
            {
                EnableFlyMode(false);
            }
        }
    }
    public void Fly()
    {
        float FlyY = flyingHigh + wingUpperBone.transform.localRotation.x*2;
        Vector3 flyingPos = new Vector3(whoWillMove.transform.position.x, FlyY, whoWillMove.transform.position.z);
        whoWillMove.transform.position = flyingPos;
    }

    public void FaceTheMouse()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane (Vector3.up, new Vector3(0, whoWillMove.transform.position.y, 0));
        float rayLength = 200;
        if (groundPlane.Raycast(camRay, out rayLength))
        {
            pointToLookAt = Vector3.Lerp(
                pointToLookAt,
                camRay.GetPoint(rayLength),
                facingTheMouseLerp
                );
            whoWillMove.transform.LookAt(new Vector3(pointToLookAt.x, whoWillMove.transform.position.y, pointToLookAt.z) );
            Debug.DrawLine(camRay.origin, pointToLookAt, Color.green);
        }
    }

    public void InputDashKeys()
    {
        if (Input.GetButtonDown("Dash"))
        {
            PrepareTheDash();
        }

    }
    public void PrepareTheDash()
    {
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dashDistance))
        {
            Debug.Log("Dash worked");
            Dash(hit.point);
        }
        else
        {
            Dash(ray.GetPoint(dashDistance));
        }
    }

    void Dash(Vector3 dashPosition)
    {
        Instantiate(dashEffect, transform.position, transform.rotation);
        whoWillMove.transform.position = dashPosition;
    }
}