using UnityEngine;

public class RenderCamera : MonoBehaviour
{
    [SerializeField] private Camera normalCam;
    [SerializeField] private Camera camToRender;
    [SerializeField] private Camera camToRender2;
    private Renderer render;
    private Plane[] planes;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    private bool IsVisibleFrom()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(normalCam);
        return GeometryUtility.TestPlanesAABB(planes, render.bounds);
    }

    private void Update()
    {        
        if (IsVisibleFrom())
        {
            camToRender.enabled = true;
            camToRender2.enabled = true;
        }
        else
        {
            camToRender.enabled = false;
            camToRender2.enabled = false;
        }
    }
}