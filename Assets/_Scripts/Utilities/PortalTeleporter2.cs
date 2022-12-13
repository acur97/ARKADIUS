using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTeleporter2 : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		//if (canTeleport && other.tag == "Player")
		//{
		//	transition.ActivateTransition(otherDoor, transform, g180);
		//}
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(1);
		}
	}
}