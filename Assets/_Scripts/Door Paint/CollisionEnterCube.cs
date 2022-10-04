using UnityEngine;

public class CollisionEnterCube : MonoBehaviour
{
    public PaintDoor paintDoor;
    private int count = 0;

    private void Awake()
    {
        count = (int)((transform.localPosition.x * 4) + (-transform.localPosition.y * 20));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mano"))
        {
            paintDoor.ActivateParticle(count);
        }
    }
}