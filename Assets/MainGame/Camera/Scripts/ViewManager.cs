using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public Camera cam;
    public float MaxSize, MinimumSize;
    public float FOV, zoomLerp;
    private float currentFov = 0, tempLastFov = 0;

    void Start()
    {
        currentFov = FOV;
    }

    void Update()
    {
        ChangeFov();
    }
    void ChangeFov()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            ModifyFov();
        }
        if (cam.orthographicSize != currentFov)
        {
            Zoom();
        }
    }
    void ModifyFov()
    {
        currentFov += FOV * -Input.GetAxis("Mouse ScrollWheel");
        if(currentFov > MaxSize)
        {
            currentFov = MaxSize;
            return;
        }
        else if(currentFov < MinimumSize)
        {
            currentFov = MinimumSize;
        }
    }
    void Zoom()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, currentFov, zoomLerp);
    }
}
