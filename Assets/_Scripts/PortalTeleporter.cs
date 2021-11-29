using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PortalTeleporter : MonoBehaviour
{
	public float goToX;
	public float goToZ;
	public Transform player;
	public Transform reciever;

	private bool playerIsOverlapping = false;
	private CharacterController cc;
	private OVRPlayerController ovrP;

	private void Awake()
	{
		cc = player.GetComponent<CharacterController>();
		ovrP = player.GetComponent<OVRPlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (!ovrP.waiting && dotProduct < 0f)
			{
                /// Teleport him!
                //float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                //rotationDiff += 180;
                //player.Rotate(Vector3.up, rotationDiff);

                //Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                //player.position = reciever.position + positionOffset;

                cc.enabled = false;
                player.position = new Vector3(goToX, player.position.y, goToZ);
                cc.enabled = true;

				ovrP.Iteleported();

                playerIsOverlapping = false;

				Debug.LogWarning("Teleport!");
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}