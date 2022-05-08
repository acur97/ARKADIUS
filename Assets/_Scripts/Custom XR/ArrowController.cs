using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody rb;
    private Quaternion rot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rot = transform.rotation;
        if (rb.velocity != Vector3.zero)
        {
            rot.SetLookRotation(rb.velocity);
            transform.rotation = rot;
        }
    }
}