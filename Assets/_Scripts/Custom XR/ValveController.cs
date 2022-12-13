using Glitic.UI.Feature;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveController : MonoBehaviour
{
    public bool portalOpen = false;

    [Space]
    [SerializeField] private Transform root;
    [SerializeField] private Transform rootOffset;
    [SerializeField] private Transform rootAim;
    [SerializeField] private XRGrabInteractable grab;
    private Transform selectedHand;
    private readonly float min = 0;
    private readonly float max = 1080;

    [Space]
    public float lastRotation = 0;
    public float rotacion = 0;
    public float rotacionFull = 0;
    public float rotacionOffset = 0;
    public float lastRotacionOffset = 0;
    public float rotacionFinal = 0;
    [Range(0, 1)]
    public float rotacionFinalTest = 0;

    [Space]
    [SerializeField] private bool agarrado;

    [Space]
    [SerializeField] ParticleSystem particle1;
    private ParticleSystem.MainModule main1;
    [SerializeField] ParticleSystem particle2;
    private ParticleSystem.MainModule main2;
    [SerializeField] ParticleSystem particle3;
    private ParticleSystem.MainModule main3;

    [Space]
    [SerializeField] private Slider slider;
    [SerializeField] private UIGradient fill;
    [SerializeField] private Image handle;

    private void Start()
    {
        main1 = particle1.main;
        main2 = particle2.main;
        main3 = particle3.main;

        main1.startLifetime = 0;
        main2.startLifetime = 0;
        main3.startLifetime = 0;

        main1.startLifetime = Mathf.Clamp(rotacionFinal.Remap(0, 1080, 0, 5), 0, 1080);
        main2.startLifetime = Mathf.Clamp(rotacionFinal.Remap(0, 1080, 0, 5), 0, 1080);
        main3.startLifetime = Mathf.Clamp(rotacionFinal.Remap(0, 1080, 0, 8), 0, 1080);

        slider.value = rotacionFinalTest;

        rotacionFinalTest = Mathf.Clamp01(rotacionFinal.Remap(0, 1080, 0, 1));

        handle.color = Color.Lerp(fill.m_color2, fill.m_color1, rotacionFinalTest);
    }

    private void OnEnable()
    {
        grab.selectEntered.AddListener(OnSelected);
    }
    private void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        selectedHand = args.interactorObject.transform;
        rotacionOffset = rotacion;
    }

    private void Update()
    {
        rotacionFull = rootAim.localEulerAngles.y;

        rotacion += rotacionFull - lastRotation;

        if (rotacionFull != lastRotation)
        {
            if (agarrado)
            {
                if (lastRotation > 270 && lastRotation <= 360 && rotacionFull >= 0 && rotacionFull < 90)
                {
                    rotacion += 360;
                }
                else if (lastRotation >= 0 && lastRotation < 90 && rotacionFull > 270 && rotacionFull <= 360)
                {
                    rotacion -= 360;
                }

                rotacionFinal = Mathf.Clamp(rotacion - rotacionOffset, 0, 10800000);
            }
            else
            {
                rotacionOffset += rotacionFull - lastRotation;
                rotacionOffset -= lastRotacionOffset;
            }
        }

        if (rotacion > 1080 && agarrado)
        {
            rotacion = 1080;
            rotacionOffset = lastRotacionOffset;
        }
        else if (rotacion < 0 && agarrado)
        {
            rotacion = 0;
            rotacionOffset = lastRotacionOffset;
        }

        lastRotacionOffset = rotacionOffset;
        lastRotation = rotacionFull;

        // 3 vueltas = 1080
        main1.startLifetime = Mathf.Clamp(rotacionFinal.Remap(min, max, 0, 5), min, max);
        main2.startLifetime = Mathf.Clamp(rotacionFinal.Remap(min, max, 0, 5), min, max);
        main3.startLifetime = Mathf.Clamp(rotacionFinal.Remap(min, max, 0, 8), min, max);

        rotacionFinalTest = Mathf.Clamp01(rotacionFinal.Remap(min, max, 0, 1));

        handle.color = Color.Lerp(fill.m_color2, fill.m_color1, rotacionFinalTest);

        slider.value = rotacionFinalTest;

        rootOffset.localEulerAngles = new Vector3(-90, Mathf.Clamp(rotacionFinal, min, max), 0);

        if (rotacionFinalTest >= 0.89f)
        {
            portalOpen = true;
        }
        else
        {
            portalOpen = false;
        }

        if (grab.isSelected)
        {
            root.position = selectedHand.position;

            // rotaciones de arriba
        }
    }
}