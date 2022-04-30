using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    [SerializeField] private bool disabler = true;
    [SerializeField] private Camera camToRender;
    [SerializeField] private Camera camToRender2;

    private void Awake()
    {
        if (disabler)
        {
            camToRender.enabled = false;
            camToRender2.enabled = false;
        }
        else
        {
            camToRender.enabled = true;
            camToRender2.enabled = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (disabler)
        {
            camToRender.enabled = false;
            camToRender2.enabled = false;
        }
    }

    private void OnBecameVisible()
    {
        camToRender.enabled = true;
        camToRender2.enabled = true;
    }
}