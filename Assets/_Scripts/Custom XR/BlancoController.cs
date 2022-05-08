using UnityEngine;

public class BlancoController : MonoBehaviour
{
    [SerializeField] private ArcoController aController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flecha"))
        {
            aController.tirosEnBlanco++;
            gameObject.SetActive(false);
        }
    }
}