using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Transform ourCursor;

    void Awake()
    {
        DontDestroyOnLoad(transform);
    }

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
        Vector2 cursorPose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ourCursor.position = cursorPose;
    }
}
