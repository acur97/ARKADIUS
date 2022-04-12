using UnityEngine;

public class RotationConstraintEuler : MonoBehaviour
{
    [SerializeField] private Transform source;

    public bool canUpdate = false;
    public float totalRotation = 0;

    private Vector3 lastPoint = Vector3.zero;

    private void Update()
    {
        if (canUpdate)
        {
            Vector3 facing = source.TransformDirection(Vector3.forward);
            facing.x = 0;

            float angle = Vector3.Angle(lastPoint, facing);
            if (Vector3.Cross(lastPoint, facing).x < 0)
                angle *= -1;

            totalRotation += angle;
            lastPoint = facing;

            transform.localEulerAngles = new Vector3(totalRotation, 0, 0);
        }
    }

    public void ResetValues()
    {
        totalRotation = 0;
        lastPoint = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}