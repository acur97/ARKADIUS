using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SonidosInicio : MonoBehaviour
{
    private float speed;
    [System.Serializable]
    public class ComboSonidos
    {
        public AudioSource sonido;
        public ParticleSystem particulas;
    }
    public GameObject[] Interactuables;
    public ComboSonidos[] combos;

    float SoundBegin = 5;
    float Megatimer = 10;
    bool Tr2 = true;
    bool Tr = true;
    bool Tr3 = true;
    bool Tr4 = true;
    bool Tr5 = true;
    bool Act = true;
    bool Tr6 = true;
    bool flag = true;
    bool Conv = true;

  
    private void Update()
    {
         
    }


   public  IEnumerator ChangeSpeed(float v_start, float v_end, float duration ,AudioSource _Sonidos)
    {
        if (_Sonidos != null)
        {
            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
                elapsed += Time.deltaTime;
                _Sonidos.volume = speed;
                yield return null;
            }
            speed = v_end;
            _Sonidos.volume = speed;
        }
    }

    public void BajarRadio()
    {
        StartCoroutine(ChangeSpeed(1, 0, 5, combos[1].sonido));
        if(combos[1].sonido.volume <= 0.4) { Tr3 = false;  Tr2 = false; }
    }

    public void BajarGeneral()
    {
        combos[0].sonido.volume -= 0.2f;
    }

    public void BajarPlaza2()
    {           
        combos[2].sonido.volume -= 0.31f;
    }

    public void BajarPlaza1()
    {
        combos[1].sonido.volume -= 0.31f;
    }

    public void BajarLicuadora()
    {
        AudioSource X = Interactuables[1].GetComponent<AudioSource>();
        X.volume -= 0.2f;
        
    }

    public void ActivarConversacion()
    {
        
        if (Conv)
        {
            Conv = false;
            combos[5].sonido.Play();
            StartCoroutine(ChangeSpeed(1, 0, 50, combos[5].sonido));
        }
   
    }

    public void ActivarMegafono()
    {   
        
        Interactuables[5].SetActive(true);
   
    }


    public void ActivarSonidos(int Activate)
    {
        if(Interactuables[Activate]!= null)
        {
            Interactuables[Activate].SetActive(true);
        }
    }


    public void ActivarObjetos()
    {
        for (int i = 0; i < Interactuables.Length; i++)
        {
            Interactuables[i].SetActive(true);
        }
    }

    public void SecuenciaPlaza()
    {
        if (Tr2)
        {
            StartCoroutine(ChangeSpeed(0, 1, 25, combos[0].sonido));
            StartCoroutine(ChangeSpeed(.5f, 1, 15, combos[1].sonido));
            StartCoroutine(ChangeSpeed(.5f, 1, 25, combos[2].sonido));
            StartCoroutine(ChangeSpeed(.5f, 1, 15, combos[3].sonido));
            StartCoroutine(ChangeSpeed(.5f, 1, 10, combos[4].sonido));
            Tr2 = false;
        }
    }
}
