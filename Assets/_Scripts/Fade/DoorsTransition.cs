using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DoorsTransition : MonoBehaviour
{
    [SerializeField] private Volume volume;

    [Header("Transforms")]
    [SerializeField] private Transform XRorigin;
    private Transform XRoriginParent;
    [SerializeField] private Transform cameraOffset;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform moveTo;
    [SerializeField] private Transform teleportTo;
    [SerializeField] private Vector3 teleportToOffset = Vector3.zero;
    private Vector3 teleportOffset;
    private Vector3 newXRpos = Vector3.zero;

    [Header("Variables")]
    [SerializeField] private AnimationCurve smooth;
    [SerializeField] private float speedTransitionUp = 0.5f;
    [SerializeField] private float speedTransitionDown = 0.25f;
    [SerializeField] private float speedTransitionMove = 0.04f;

    public bool fade = false;
    private int change = 0;
    private int change2 = 0;
    private bool up = true;
    private float val = 0;
    private float valCurve = 0;
    private Vector4 val4 = Vector4.zero;
    private LiftGammaGain liftGammaGain;

    private void Awake()
    {
        volume.profile.TryGet(out liftGammaGain);
        XRoriginParent = XRorigin.parent;
    }

    public void ActivateTransition(Transform TeleportTo, Transform door, bool giro180)
    {
        fade = false;
        teleportTo = TeleportTo;
        moveTo = door;
        if (giro180)
        {
            teleportOffset = -teleportToOffset;
        }
        else
        {
            teleportOffset = teleportToOffset;
        }
        change = 0;
        change2 = 0;
        val = 0;
        valCurve = 0;
        up = true;
        fade = true;
    }

    private void Update()
    {
        if (fade)
        {
            change++;
            if (up)
            {
                val += Time.deltaTime * speedTransitionUp;
                if (val > 1)
                {
                    up = false;
                }
            }
            else
            {
                val -= Time.deltaTime * speedTransitionDown;
                change2++;
                if (val <= 0)
                {
                    fade = false;
                    up = true;
                }
            }

            valCurve = smooth.Evaluate(Mathf.Clamp01(val));

            val4 = new Vector4(1, 1, 1, valCurve);
            liftGammaGain.lift.value = val4;
            liftGammaGain.gamma.value = val4;
            liftGammaGain.gain.value = val4;

            if (change == 1)
            {
                cameraOffset.SetParent(XRorigin.parent);
                XRorigin.SetParent(mainCamera);
                XRorigin.localPosition = Vector3.zero;
                XRorigin.SetParent(XRoriginParent);
                XRorigin.SetSiblingIndex(2);
                cameraOffset.SetParent(XRorigin);
                cameraOffset.SetSiblingIndex(1);
                cameraOffset.localPosition = new Vector3(cameraOffset.localPosition.x, 0, cameraOffset.localPosition.z);
            }
            if (change2 == 1)
            {
                XRorigin.position = teleportTo.position;
                XRorigin.position = new Vector3(XRorigin.position.x + teleportOffset.x, 0, XRorigin.position.z + teleportOffset.z);
                XRorigin.position += cameraOffset.localPosition;
                XRorigin.position = new Vector3(XRorigin.position.x, 0, XRorigin.position.z);
                XRorigin.localScale = Vector3.one;
                cameraOffset.localPosition = Vector3.zero;
                cameraOffset.localScale = new Vector3(1.4f, 1.4f, 1.4f);
            }
            else if (change2 < 1)
            {
                newXRpos = Vector3.Lerp(XRorigin.position, moveTo.position, valCurve * speedTransitionMove);
                newXRpos = new Vector3(newXRpos.x, 0, newXRpos.z);
                XRorigin.transform.position = newXRpos;
                XRorigin.transform.localPosition = new Vector3(XRorigin.transform.localPosition.x, 0, XRorigin.transform.localPosition.z);
            }
        }
    }
}