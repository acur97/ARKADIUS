using UnityEngine;

public class OvrCeroLimiter : MonoBehaviour
{
    public bool active = false;

    private void Update()
    {
        if (active && transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}