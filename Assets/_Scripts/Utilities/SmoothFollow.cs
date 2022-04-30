using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public enum TipoVista { Ojos, Cara, Cuerpo }
    public TipoVista vista;
    public Transform toFollow;
    public bool following = false;

    [Space]
    [SerializeField] private float velocity = 0.5f;
    [SerializeField] private float smoothOjos = 5;
    [SerializeField] private float smoothCara = 2.5f;
    [SerializeField] private float smoothCuerpo = 1;

    [Space]
    [SerializeField] private Transform vistaOjos;
    [SerializeField] private Transform vistaCara;
    [SerializeField] private float t = 0;

    [Space]
    public bool resetStartPos = true;
    [SerializeField] private Transform startPosition;

    private Transform lastFollow;
    private Vector3 desiredPositionOjos;
    private Vector3 desiredPositionCara;
    private Vector3 desiredPositionCuerpo;

    private void Awake()
    {
        lastFollow = startPosition;
    }

    public void Mirar(Transform _toFollow, TipoVista _vista = TipoVista.Cuerpo)
    {
        toFollow = _toFollow;
        vista = _vista;
    }

    private void Update()
    {
        if (lastFollow != toFollow)
        {
            following = true;
            t = 0;
        }

        if (following)
        {
            if (t == 1)
            {
                t = 0;
            }

            t += Time.deltaTime * velocity;

            if (t > 1)
            {
                following = false;
            }

            t = Mathf.Clamp01(t);
        }

        if (toFollow != null)
        {
            desiredPositionOjos = Vector3.Lerp(vistaOjos.position, toFollow.position, t);
        }
        else if (resetStartPos)
        {
            desiredPositionOjos = Vector3.Lerp(vistaOjos.position, startPosition.position, t);
        }
        else
        {
            following = false;
        }

        vistaOjos.position = Vector3.Lerp(vistaOjos.position, desiredPositionOjos, smoothOjos * Time.deltaTime);
        vistaOjos.localPosition = new Vector3(vistaOjos.localPosition.x, vistaOjos.localPosition.y, Mathf.Clamp(vistaOjos.localPosition.z, 0.5f, 1000));

        switch (vista)
        {
            //case TipoVista.Ojos:
            //    break;

            //case TipoVista.Cara:
            //    desiredPositionCara = Vector3.Lerp(desiredPositionCara, desiredPositionOjos, smoothCuerpo * Time.deltaTime);
            //    vistaCara.LookAt(desiredPositionCara);
            //    vistaCara.localEulerAngles = new Vector3(0/*clamp*/, 0/*clamp*/, 0);
            //    break;

            case TipoVista.Cuerpo:
                desiredPositionCuerpo = Vector3.Lerp(desiredPositionCuerpo, desiredPositionOjos, smoothCuerpo * Time.deltaTime);
                transform.LookAt(desiredPositionCuerpo);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                break;
        }

        lastFollow = toFollow;
    }
}