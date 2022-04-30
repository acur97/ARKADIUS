using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DirectionalLightEnabler : MonoBehaviour
{
    [SerializeField] private float renderLimit = 2500;
    [SerializeField] private float renderLimitLight = 1000;
    [SerializeField] private Transform gameCamera;
    [SerializeField] private GameObject root;
    [SerializeField] private Light dirLight;

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
        }
        else
        {
            //root.SetActive(true);
            if (Vector3.Distance(transform.position, SceneView.lastActiveSceneView.camera.transform.position) <= renderLimitLight)
            {
                dirLight.shadows = LightShadows.Soft;
            }
            else
            {
                dirLight.shadows = LightShadows.None;
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
#endif
    }
}