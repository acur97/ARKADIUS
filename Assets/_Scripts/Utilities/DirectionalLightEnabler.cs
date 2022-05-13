using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DirectionalLightEnabler : MonoBehaviour
{
    [SerializeField] private float renderLimit = 2500;
    [SerializeField] private Transform gameCamera;
    [SerializeField] private GameObject root;

    [Space]
    [SerializeField] private float renderLimitLight = 1000;
    [SerializeField] private Light dirLight;

    [Space]
    [SerializeField] private float renderLimitAmbient = 1000;
    [SerializeField, ColorUsage(false, true)] private Color skyColor = new Color(0.212f, 0.227f, 0.259f);
    [SerializeField, ColorUsage(false, true)] private Color equatorColor = new Color(0.114f, 0.125f, 0.133f);
    [SerializeField, ColorUsage(false, true)] private Color groundColor = new Color(0.047f, 0.043f, 0.035f);

    [Space]
    [SerializeField] private float renderLimitCamEnablers = 500;
    [SerializeField] private RenderCamera rCamera1;
    [SerializeField] private RenderCamera rCamera2;

    private void Update()
    {

#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimit)
            {
                root.SetActive(true);
            }
            else
            {
                root.SetActive(false);
            }

            if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitLight)
            {
                dirLight.shadows = LightShadows.Soft;
            }
            else
            {
                dirLight.shadows = LightShadows.None;
            }

            if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitAmbient)
            {
                RenderSettings.ambientSkyColor = skyColor;
                RenderSettings.ambientEquatorColor = equatorColor;
                RenderSettings.ambientGroundColor = groundColor;
            }

            if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitCamEnablers)
            {
                if (rCamera1 != null)
                    rCamera1.enabled = true;
                if (rCamera2 != null)
                    rCamera2.enabled = true;
            }
            else
            {
                if (rCamera1 != null)
                    rCamera1.enabled = false;
                if (rCamera2 != null)
                    rCamera2.enabled = false;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, SceneView.lastActiveSceneView.camera.transform.position) <= renderLimitLight)
            {
                dirLight.shadows = LightShadows.Soft;
            }
            else
            {
                dirLight.shadows = LightShadows.None;
            }
            if (Vector3.Distance(transform.position, SceneView.lastActiveSceneView.camera.transform.position) <= renderLimitAmbient)
            {
                RenderSettings.ambientSkyColor = skyColor;
                RenderSettings.ambientEquatorColor = equatorColor;
                RenderSettings.ambientGroundColor = groundColor;
            }
        }

#else
        if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimit)
        {
            root.SetActive(true);
        }
        else
        {
            root.SetActive(false);
        }

        if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitLight)
        {
            dirLight.shadows = LightShadows.Soft;
        }
        else
        {
            dirLight.shadows = LightShadows.None;
        }  
        
        if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitAmbient)
        {
            RenderSettings.ambientSkyColor = skyColor;
            RenderSettings.ambientEquatorColor = equatorColor;
            RenderSettings.ambientGroundColor = groundColor;
        }

        if (Vector3.Distance(transform.position, gameCamera.position) <= renderLimitCamEnablers)
        {
            if (rCamera1 != null)
                rCamera1.enabled = true;
            if (rCamera2 != null)
                rCamera2.enabled = true;
        }
        else
        {
            if (rCamera1 != null)
                rCamera1.enabled = false;
            if (rCamera2 != null)
                rCamera2.enabled = false;
        }
#endif

    }
}