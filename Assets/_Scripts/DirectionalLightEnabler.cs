using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class DirectionalLightEnabler : MonoBehaviour
{
    [SerializeField] private Transform gameCamera;
    [SerializeField] private new GameObject light;

    private void Update()
    {
        #if UNITY_EDITOR
        if (Application.isPlaying)
        {
            if (Vector3.Distance(transform.position, gameCamera.position) <= 1000)
            {
                light.SetActive(true);
            }
            else
            {
                light.SetActive(false);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, SceneView.lastActiveSceneView.camera.transform.position) <= 1000)
            {
                light.SetActive(true);
            }
            else
            {
                light.SetActive(false);
            }
        }
        #else
        if (Vector3.Distance(transform.position, gameCamera.position) <= 1000)
        {
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
        }
        #endif
    }
}