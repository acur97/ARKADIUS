using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuntoAPunto : MonoBehaviour
{
    public float velocidad = 0.01f;
    [System.Serializable]
    public class AtoB
    {
        public Transform start;
        public Transform final;
        public Transform _object;
        public UnityEvent OnEnd;
    }
    public AtoB[] atoBs;

    private int contador = 0;
    private float valor = 0;
    private bool moviento = true;

    private void Update()
    {
        if (moviento)
        {
            atoBs[contador]._object.position = Vector3.Lerp(atoBs[contador].start.position, atoBs[contador].final.position, valor);
            valor += velocidad * Time.deltaTime;
            if (valor >= 1)
            {
                atoBs[contador].OnEnd.Invoke();
                contador += 1;
                valor = 0;
                if (contador == atoBs.Length)
                {
                    moviento = false;
                }
            }
        }
    }
}