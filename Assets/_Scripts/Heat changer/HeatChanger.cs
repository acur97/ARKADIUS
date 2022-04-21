using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class HeatChanger : MonoBehaviour
{
    public static HeatChanger Instance;

    [Range(0f, 1f)]
    public float heat = 0;
    private float lastHeat = 0;
    private float invertHeat = 1;

    [System.Serializable] private class Blends
    {
        public Material mat;
        public ReflectionProbe probe1;
        public float intensity1 = 2;
        public ReflectionProbe probe2;
        public float intensity2 = 2;
    }
    [Space]
    [SerializeField] Blends[] blends;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private WindZone zone;
    [SerializeField] private Volume volume;
    [SerializeField] private AnimationCurve truenos;
    private float truenosVal = 0;
    private float truenosVal2 = 0;
    private WaitForSeconds wait;
    private ColorAdjustments colorAdjustments;

    private const string _Blend = "_Blend";
    private const string _Vol_Musica_Fuerte = "Vol_Musica_Fuerte";
    private const string _Vol_Musica_Suave = "Vol_Musica_Suave";

    private void Awake()
    {
        Instance = this;

        volume.profile.TryGet(out colorAdjustments);

        wait = new WaitForSeconds(0.5f);
    }

    private void Start()
    {
        lastHeat = heat;
        invertHeat = heat.Remap(0, 1, 1, 0);
        Change();
        StartCoroutine(TruenosPausa());
    }    

    private void Change()
    {
        for (int i = 0; i < blends.Length; i++)
        {
            blends[i].mat.SetFloat(_Blend, heat);
            blends[i].probe1.intensity = invertHeat * blends[i].intensity1;
            blends[i].probe2.intensity = heat * blends[i].intensity2;
        }
        mixer.SetFloat(_Vol_Musica_Suave, ToVolume(invertHeat));
        mixer.SetFloat(_Vol_Musica_Fuerte, ToVolume(heat));
        zone.windMain = heat;
        volume.weight = heat;
    }

    private void Update()
    {
        invertHeat = heat.Remap(0, 1, 1, 0);
        if (heat != lastHeat)
        {
            Change();
        }
        lastHeat = heat;

        truenosVal += (Time.deltaTime * 2) * truenosVal2;
        if (truenosVal >= 1)
        {
            truenosVal = 0;
        }

        colorAdjustments.postExposure.value = truenos.Evaluate(truenosVal) * heat - 1;
    }

    private float ToVolume(float value)
    {
        return Mathf.Log(Mathf.Clamp(value, 0.0001f, 1)) * 20;
    }

    private IEnumerator TruenosPausa()
    {
        truenosVal2 = 0;
        yield return new WaitForSeconds(Random.Range(1, 10));
        truenosVal2 = 1;
        yield return wait;
        StartCoroutine(TruenosPausa());
    }
}