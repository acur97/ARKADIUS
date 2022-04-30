#if !UNITY_EDITOR
using Unity.XR.Oculus;
#endif
using UnityEngine;

public class FFRendering : MonoBehaviour
{
#if !UNITY_EDITOR
    private void Awake()
    {
        Utils.SetFoveationLevel(2);
    }
#endif
}