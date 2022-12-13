using UnityEngine;

[ExecuteInEditMode]
public class StopLocalTransform : MonoBehaviour
{
    public bool x;
    public bool y;
    public bool z;
    private Vector3 start;

    private void Start()
    {
        start = transform.localPosition;
    }

    private void Update()
    {
        if (x)
        {
            transform.localPosition = new Vector3(start.x, transform.localPosition.y, transform.localPosition.z);
        }
        if (y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, start.y, transform.localPosition.z);
        }
        if (z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, start.z);
        }
    }
}