using UnityEngine;
using UnityEngine.Animations;

[ExecuteInEditMode]
public class ArcoController : MonoBehaviour
{
    [SerializeField] private Transform door;

    [Space]
    [SerializeField] private LineRenderer lRender;
    [SerializeField] private Transform root0;
    [SerializeField] private Transform root1;
    [SerializeField] private Transform root2;
    [SerializeField] private Transform rootFlecha;
    [SerializeField] private GameObject flecha;
    private GameObject obF;
    private Rigidbody rb;
    private LookAtConstraint lk;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private Transform to2;

    [Space]
    [SerializeField] private float limitEstiro = 1.4f;
    [SerializeField] private float powerDisparo;
    [SerializeField] private Vector3 centerMass;

    [Space]
    [SerializeField] private Transform rootCentro;

    private bool veniaDeAgarre = false;

    [Space]
    public int tirosEnBlanco;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            flecha.SetActive(false);
            ResetResorte();
        }
    }

    private void Update()
    {
        if (tirosEnBlanco == 1)
        {
            door.localEulerAngles = new Vector3(0, 0, 30);
        }
        else if (tirosEnBlanco == 2)
        {
            door.localEulerAngles = new Vector3(0, 0, 60);
        }
        else if (tirosEnBlanco == 3)
        {
            door.localEulerAngles = new Vector3(0, 0, 90);
        }

        lRender.SetPosition(0, root0.position);
        lRender.SetPosition(1, root1.position);
        lRender.SetPosition(2, root2.position);

        rootFlecha.position = root1.position;

        to.position = to2.position;
        from.localPosition = Vector3.ClampMagnitude(to.localPosition, limitEstiro);
    }

    public void InstantiateFlecha()
    {
        veniaDeAgarre = true;

        obF = GameObject.Instantiate(flecha, Vector3.zero, Quaternion.identity, rootFlecha);
        obF.transform.localPosition = Vector3.zero;
        obF.transform.localRotation = Quaternion.identity;
        obF.SetActive(true);
        rb = obF.GetComponent<Rigidbody>();
        rb.centerOfMass = centerMass;
        lk = obF.GetComponent<LookAtConstraint>();
    }

    public void ResetResorte()
    {
        if (veniaDeAgarre)
        {
            lk.enabled = false;
            obF.transform.SetParent(transform.parent);
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddRelativeForce(0, 0, Vector3.Distance(rootCentro.localPosition, from.localPosition) * powerDisparo);

            veniaDeAgarre = false;
        }

        to2.position = from.position;
    }
}