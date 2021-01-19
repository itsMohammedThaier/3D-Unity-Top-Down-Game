using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Transform ourCursor;

    void Update()
    {
        CustomCursorActions();
    }
    private void CustomCursorActions()
    {
        turnMouseVisibilty();
        FollowCursorPose();
    }
    private void turnMouseVisibilty()
    {
        if (Cursor.visible == true)
            Cursor.visible = false;
    }
    private void FollowCursorPose()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayLenght = 10;
        ourCursor.position = camRay.GetPoint(1);



        // Vector2 cursorPose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ourCursor.position = new Vector2(cursorPose.x, cursorPose.y);
    }
}
