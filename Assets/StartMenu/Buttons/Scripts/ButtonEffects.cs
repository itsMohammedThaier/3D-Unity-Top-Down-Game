using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffects : MonoBehaviour
{
    public Transform board;
    public float movingLerp;
    private bool isHovered;
    private bool isClicked;

    // Update is called once per frame
    void Update()
    {
        DoHoverEffect();
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
            //TODO: Make the click effect
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
