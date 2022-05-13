using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
	[SerializeField] private DoorsTransition transition;

	[Space]
	public bool canTeleport = false;
	public bool g180 = false;
	[SerializeField] private Transform otherDoor;

	void OnTriggerEnter(Collider other)
	{
		if (canTeleport && other.tag == "Player")
		{
			transition.ActivateTransition(otherDoor, transform, g180);
		}
	}
}