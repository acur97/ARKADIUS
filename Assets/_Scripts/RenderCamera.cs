using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    [SerializeField] private bool disabler = true;
    [SerializeField] private Camera camToRender;

    private void Awake()
    {
        if (disabler)
            camToRender.enabled = false;
        else
            camToRender.enabled = true;
    }

    private void OnBecameInvisible()
    {
        if (disabler)
            camToRender.enabled = false;
    }

    private void OnBecameVisible()
    {
        camToRender.enabled = true;
    }
}