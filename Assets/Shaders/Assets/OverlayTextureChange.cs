using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class OverlayTextureChange : MonoBehaviour
{
    public ScreenOverlay screenO;
    public float interval = 0.02f;
    public Texture2D[] textures = new Texture2D[0];
    private int cantidadText = 0;

    private const string _CambioTextura = "CambioTextura";

    private void Start()
    {
        InvokeRepeating(_CambioTextura, 0, interval);
    }

    public void CambioTextura()
    {
        screenO.texture = textures[cantidadText];
        cantidadText++;
        if (cantidadText == textures.Length)
        {
            cantidadText = 0;
        }
    }
}