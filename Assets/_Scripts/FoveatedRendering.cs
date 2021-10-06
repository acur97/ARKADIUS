using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRManager;

public class FoveatedRendering : MonoBehaviour
{
    public FixedFoveatedRenderingLevel Rlevel = FixedFoveatedRenderingLevel.Low;
    public bool DynamicFixedFoveatedRendering = true;

    private void Awake()
    {
        OVRManager.fixedFoveatedRenderingLevel = Rlevel; // it's the maximum foveation level
        OVRManager.useDynamicFixedFoveatedRendering = true;
    }
}