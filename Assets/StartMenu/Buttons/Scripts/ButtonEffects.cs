using UnityEngine;

public class ButtonEffects : MonoBehaviour
{
    public Transform board;
    public float movingLerp;
    private bool isHovered;
    private bool isClicked;

    private Transform theWholeObject;
    public float clickSizeLerp;
    private Vector3 defaultSize;
    public Vector3 clickedSize;

    void Start()
    {
        theWholeObject = GetComponent<Transform>();
        defaultSize = theWholeObject.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        DoHoverEffect();
        ClickEffect();
    }

    public void DoHoverEffect()
    {
        Vector3 unHoveredScale = new Vector3(0, board.transform.localScale.y, board.transform.localScale.z);
        Vector3 hoveredScale = new Vector3(1, board.transform.localScale.y, board.transform.localScale.z);
        if (isHovered)
        {
            board.transform.localScale = Vector3.Lerp(board.transform.localScale, hoveredScale, movingLerp);
        }
        else
        {
            board.transform.localScale = Vector3.Lerp(board.transform.localScale, unHoveredScale, movingLerp);
        }
    }
    public void turnEffectOn()
    {
        isHovered = true;
    }
    public void turnEffectOff()
    {
        isHovered = false;
    }

    public void ClickEffect()
    {
        if (isClicked)
        {
            theWholeObject.transform.localScale = Vector3.Lerp(theWholeObject.localScale, clickedSize, clickSizeLerp);
        }
        else
        {
            theWholeObject.transform.localScale = Vector3.Lerp(theWholeObject.localScale, defaultSize, clickSizeLerp);
        }
    }
    public void TurnOnIsClicked()
    {
        isClicked = true;
    }
    public void TurnOffIsClicked()
    {
        isClicked = false;
    }
}
