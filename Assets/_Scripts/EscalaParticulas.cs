using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalaParticulas : MonoBehaviour
{
    public Transform esfera;
    public ParticleSystem ps;
    public AudioSpectrum aSpectrum;
    public AudioSource aSource;
    public float multiplicador = 1;
    public float _base = 0.25f;

    private float valor;

    private void Update()
    {
        valor = (_base + (aSpectrum.amplitudeBuffer * multiplicador)) * aSource.volume;

        if (float.IsNaN(valor))
        {
            valor = 0;
        }

        esfera.localScale = new Vector3(valor, valor, valor);
        var sh = ps.shape;
        var mn = ps.main;
        sh.radius = valor;
        mn.startSpeed = ((valor - _base) * 2) * aSource.volume;
    }
}