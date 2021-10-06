using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionParticulas : MonoBehaviour
{
    public AudioSpectrum Aspectrum;
    private Transform ps;
    public float multiplicador;

    private float velocidad;

    private void Awake()
    {
        ps = transform;
    }

    void FixedUpdate()
    {
        velocidad = Aspectrum.amplitudeBuffer * multiplicador;

        if (float.IsNaN(velocidad))
        {
            velocidad = 0;
        }

        ps.localPosition = new Vector3(0, 0, velocidad);
    }
}