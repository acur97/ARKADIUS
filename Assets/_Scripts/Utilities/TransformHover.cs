using UnityEngine;

public class TransformHover : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smooth;
    [SerializeField] private AnimationCurve curve;
    private bool up = false;
    private float t = 0;

    private void Update()
    {
        if (up)
        {
            t += Time.deltaTime * smooth;
            if (t > 1)
            {
                up = false;
            }
        }
        else
        {
            t -= Time.deltaTime * smooth;
            if (t < 0)
            {
                up = true;
            }
        }

        root.localPosition = offset * curve.Evaluate(t);
    }
}