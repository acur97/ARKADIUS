using UnityEngine;

[ExecuteInEditMode]
public class CopyRotation : MonoBehaviour
{
    [SerializeField] private Transform copy;

    private void Update()
    {
        transform.rotation = copy.rotation;
    }
}