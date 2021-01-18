using UnityEngine;

public class ButtonsActions : MonoBehaviour
{
    public Animator sceneTransition;

    public void ChangeScene()
    {
        //In the end of this animtion the object which include the animation
        //will call a scene chaning method by an event
        sceneTransition.SetTrigger("Start");
    }
}