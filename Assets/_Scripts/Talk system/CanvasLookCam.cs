using UnityEngine;

public class CanvasLookCam : MonoBehaviour
{
    public Transform cam;
    public bool fullRotation = false;

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    private void Update()
    {
        transform.LookAt(cam);
        if (!fullRotation)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}