using UnityEngine;

public class PhysicsMover : MonoBehaviour
{
    private Rigidbody rg;

    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
    }

    public void MoveTo(Vector3 position)
    {
        rg.MovePosition(position);
    }
}