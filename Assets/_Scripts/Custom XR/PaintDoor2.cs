using UnityEngine;

public class PaintDoor2 : MonoBehaviour
{
    public Transform particle;
    public float offset = 1.5f;

    //private void Awake()
    //{
    //    particle.gameObject.SetActive(false);
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Mano"))
        {
            particle.position = new Vector3(other.transform.position.x - offset, other.transform.position.y, other.transform.position.z);
            //particle.localPosition = new Vector3(particle.localPosition.x, particle.localPosition.y, -0.11f);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Mano"))
    //    {
    //        particle.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Mano"))
    //    {
    //        particle.gameObject.SetActive(false);
    //    }
    //}
}