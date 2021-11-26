using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    public Camera camToRender;

    private void Awake()
    {
        camToRender.enabled = false;
    }

    private void OnBecameInvisible()
    {
        camToRender.enabled = false;
    }

    private void OnBecameVisible()
    {
        camToRender.enabled = true;
    }
}
