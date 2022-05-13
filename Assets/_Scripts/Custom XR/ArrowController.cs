using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float duracion = 10;

    private Rigidbody rb;
    private Quaternion rot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        duracion -= Time.deltaTime;
        if (duracion < 0)
        {
            Destroy(gameObject);
        }
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