using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private bool followEditor = true;
    [SerializeField] private Camera centerEyeCamera;
    [SerializeField] private Camera gameCamera;

    [Space]
    [SerializeField] Vector3 Offset = Vector3.zero;
    [SerializeField] Vector3 OffsetRot = Vector3.zero;

    [Space]
    [SerializeField] private Vector2 screenRes = Vector2.zero;
    [SerializeField] private bool secondEye = false;
    [SerializeField] private VRTextureUsage vrFormat;
    [SerializeField] private RenderTexture rTexture;
    [SerializeField] private Material material;
    private RenderTexture rt;
    private RenderTexture rt2;
    private readonly int _Texture2D = Shader.PropertyToID("_Texture2D");
    private readonly int _Texture2D2 = Shader.PropertyToID("_Texture2D2");

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        cam.projectionMatrix = centerEyeCamera.projectionMatrix;
        //cam.fieldOfView = centerEyeCamera.fieldOfView;

        transform.position = gameCamera.transform.position + Offset;
        transform.eulerAngles = gameCamera.transform.eulerAngles + OffsetRot;

        if (XRSettings.eyeTextureWidth > 0 && screenRes.x == 0)
        {
            screenRes = new Vector2(XRSettings.eyeTextureWidth, XRSettings.eyeTextureHeight);
            if (secondEye)
            {
                rt2 = new RenderTexture((int)screenRes.x, (int)screenRes.y, 0);
                rt2.vrUsage = vrFormat;
                cam.targetTexture = rt2;
                material.SetTexture(_Texture2D2, rt2);
            }
            else
            {
                rt = new RenderTexture((int)screenRes.x, (int)screenRes.y, 0);
                rt.vrUsage = vrFormat;
                cam.targetTexture = rt;
                material.SetTexture(_Texture2D, rt);
            }
        }
    }
}