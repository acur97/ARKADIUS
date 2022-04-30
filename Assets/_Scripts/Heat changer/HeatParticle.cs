using UnityEngine;

[ExecuteInEditMode]
public class HeatParticle : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float rateOverTime = 10;

    private ParticleSystem particle;
    private ParticleSystem.EmissionModule module;
    private HeatChanger changer;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        if (Application.isPlaying)
        {
            module = particle.emission;
        }
    }

    private void Start()
    {
        changer = HeatChanger.Instance;

        if (Application.isPlaying)
        {
            module.rateOverTime = rateOverTime;
        }
        particle.Play();
    }

    private void Update()
    {
        if (changer == null)
        {
            changer = HeatChanger.Instance;
        }

        if (!Application.isPlaying)
        {
            module = particle.emission;
        }
        module.rateOverTime = curve.Evaluate(changer.heat).Remap(0, 1, 0, rateOverTime);
    }
}