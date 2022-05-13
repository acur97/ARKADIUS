using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float resScale = 1;
    [SerializeField] private RenderTextureMemoryless rtml;
    [SerializeField] private Camera cam;
    [SerializeField] private Camera centerEyeCamera;
    [SerializeField] private Camera gameCamera;
    private Camera thisCamera;

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
        thisCamera = GetComponent<Camera>();
        if (Application.isPlaying)
        {
            thisCamera.enabled = false;
        }
    }

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += UpdateCamera;
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= UpdateCamera;
    }

    private void UpdateCamera(ScriptableRenderContext context, Camera camera)
    {
        if (thisCamera.enabled)
        {
            cam.projectionMatrix = centerEyeCamera.projectionMatrix;
            //cam.fieldOfView = centerEyeCamera.fieldOfView;
            //cam.nearClipPlane = centerEyeCamera.nearClipPlane;
            //cam.farClipPlane = centerEyeCamera.farClipPlane;

            transform.position = gameCamera.transform.position + Offset;
            transform.eulerAngles = gameCamera.transform.eulerAngles + OffsetRot;
        }

        if (Application.isPlaying && XRSettings.eyeTextureWidth > 0 && screenRes.x == 0)
        {
            screenRes = new Vector2(XRSettings.eyeTextureWidth * resScale, XRSettings.eyeTextureHeight * resScale);
            if (secondEye)
            {
                rt2 = new RenderTexture((int)screenRes.x, (int)screenRes.y, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32)
                {
                    memorylessMode = rtml,
                    anisoLevel = 0,
                    vrUsage = vrFormat
                };
                cam.targetTexture = rt2;
                material.SetTexture(_Texture2D2, rt2);
            }
            else
            {
                rt = new RenderTexture((int)screenRes.x, (int)screenRes.y, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.B10G11R11_UFloatPack32)
                {
                    memorylessMode = rtml,
                    anisoLevel = 0,
                    vrUsage = vrFormat
                };
                cam.targetTexture = rt;
                material.SetTexture(_Texture2D, rt);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (secondEye)
        {
            rt2 = rTexture;
            cam.targetTexture = rt2;
            material.SetTexture(_Texture2D2, rt2);
        }
        else
        {
            rt = rTexture;
            cam.targetTexture = rt;
            material.SetTexture(_Texture2D, rt);
        }
    }
}