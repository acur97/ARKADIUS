using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookCam : MonoBehaviour
{
    public Transform cam;

    private void Update()
    {
        transform.LookAt(cam);
    }
}