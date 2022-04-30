using UnityEngine;

[ExecuteInEditMode]
public class FollowBoxLimit : MonoBehaviour
{
    [SerializeField] private bool izq;
    [SerializeField] private Transform look;
    [SerializeField] private Transform move;
    [SerializeField] private Vector2 limitsX;
    [SerializeField] private Vector2 limitsY;

    [Space]
    [SerializeField] private bool showLimits = true;

    private Vector2 limitsXfov;
    private Vector2 limitsYfov;

    private void Update()
    {
        limitsYfov = limitsY * (look.transform.localPosition.z * 2);
        if (izq)
        {
            limitsXfov = new Vector2(limitsX.x * (look.transform.localPosition.z * 2), limitsX.y * (-look.transform.localPosition.z + 1.25f));
        }
        else
        {
            limitsXfov = new Vector2(limitsX.x * (-look.transform.localPosition.z + 1.25f), limitsX.y * (look.transform.localPosition.z * 2));
        }

        move.localPosition = new Vector2(Mathf.Clamp(look.localPosition.x, limitsXfov.x, limitsXfov.y),
            Mathf.Clamp((look.localPosition.y - transform.localPosition.y), limitsYfov.x, limitsYfov.y));

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, look.transform.localPosition.z);

        if (showLimits)
        {
            if (!move.gameObject.activeSelf)
            {
                move.gameObject.SetActive(true);
            }

            Debug.DrawLine(transform.TransformPoint(new Vector2(limitsXfov.x, limitsYfov.x)), transform.TransformPoint(new Vector2(limitsXfov.x, limitsYfov.y)), Color.red); //izq
            Debug.DrawLine(transform.TransformPoint(new Vector2(limitsXfov.y, limitsYfov.x)), transform.TransformPoint(new Vector2(limitsXfov.y, limitsYfov.y)), Color.magenta); //der
            Debug.DrawLine(transform.TransformPoint(new Vector2(limitsXfov.x, limitsYfov.x)), transform.TransformPoint(new Vector2(limitsXfov.y, limitsYfov.x)), Color.blue); //down
            Debug.DrawLine(transform.TransformPoint(new Vector2(limitsXfov.x, limitsYfov.y)), transform.TransformPoint(new Vector2(limitsXfov.y, limitsYfov.y)), Color.cyan); //up
        }
        else if (move.gameObject.activeSelf)
        {
            move.gameObject.SetActive(false);
        }
    }
}