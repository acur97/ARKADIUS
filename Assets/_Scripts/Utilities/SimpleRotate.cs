using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 speed = Vector3.zero;

    private void Update()
    {
        transform.Rotate(speed);
    }
}