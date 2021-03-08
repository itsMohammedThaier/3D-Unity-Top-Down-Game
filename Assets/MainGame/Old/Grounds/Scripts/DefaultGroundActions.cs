using UnityEngine;

public class DefaultGroundActions : MonoBehaviour
{
    public Transform WhoWillMove;
    public float MoveLerpSpeed;
    private float defaultY;
    private float highY;
    public float increasedHight;
    private bool toUpper;
    void Start()
    {
        defaultY = WhoWillMove.position.y;
        highY  = defaultY + increasedHight;
    }
    void Update()
    {
        ChangeTheHigh();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            toUpper = true;
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            toUpper = false;
        }
    }

    private void ChangeTheHigh()
    {
        Vector3 upperPos;
        if (toUpper)
        {
            upperPos = new Vector3(WhoWillMove.position.x, highY, WhoWillMove.position.z);
        }
        else
        {
            upperPos = new Vector3(WhoWillMove.position.x, defaultY, WhoWillMove.position.z);
        }

        Vector3 newPos = Vector3.Lerp(WhoWillMove.position, upperPos, MoveLerpSpeed);
        WhoWillMove.position = newPos;
    }
}