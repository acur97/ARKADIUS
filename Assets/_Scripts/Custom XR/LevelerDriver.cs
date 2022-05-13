using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LevelerDriver : MonoBehaviour
{
    [SerializeField] private XRBaseInteractable handle1;
    private Transform interactor1;
    [SerializeField] private XRBaseInteractable handle2;
    private Transform interactor2;

    [Space]
    [SerializeField] private Transform palanca1;
    [SerializeField] private Transform palanca2;
    [SerializeField] private Transform door;

    [Space]
    [SerializeField] private float speedBack;
    [SerializeField] private float multiDoor;
    private float offsetDoor;
    private bool entroOffset;
    [SerializeField] private float giro1;
    [SerializeField] private float giro2;

    [Space]
    [SerializeField] private PortalTeleporter teleporter;

    private float val1;
    private float val2;

    private void OnEnable()
    {
        handle1.selectEntered.AddListener(ActivateLookAt1);
        val1 = 300;
        handle2.selectEntered.AddListener(ActivateLookAt2);
        val2 = 60;
    }
    private void OnDisable()
    {
        handle1.selectEntered.RemoveListener(ActivateLookAt1);
        handle2.selectEntered.RemoveListener(ActivateLookAt2);
    }

    private void ActivateLookAt1(SelectEnterEventArgs args)
    {
        interactor1 = args.interactorObject.transform;
    }
    private void ActivateLookAt2(SelectEnterEventArgs args)
    {
        interactor2 = args.interactorObject.transform;
    }

    private void Update()
    {
        if (handle1.isSelected)
        {
            palanca1.LookAt(interactor1.position);
        }
        else
        {
            palanca1.Rotate(new Vector3(-speedBack * Time.deltaTime, 0, 0));
        }
        if (palanca1.localRotation.eulerAngles.x >= 300 && palanca1.localRotation.eulerAngles.x < 360)
        {
            val1 = palanca1.localRotation.eulerAngles.x;
            giro1 = val1.Remap(300, 360, 0, 0.5f);
        }
        else if (palanca1.localRotation.eulerAngles.x >= 0 && palanca1.localRotation.eulerAngles.x <= 60)
        {
            val1 = palanca1.localRotation.eulerAngles.x;
            giro1 = val1.Remap(0, 60, 0.5f, 1);
        }
        palanca1.localRotation = Quaternion.Euler(val1, 180, 0);

        
        if (handle2.isSelected)
        {
            palanca2.LookAt(interactor2.position);
        }
        else
        {
            palanca2.Rotate(new Vector3(speedBack * Time.deltaTime, 0, 0));
        }
        if (palanca2.localRotation.eulerAngles.x >= 300 && palanca2.localRotation.eulerAngles.x < 360)
        {
            val2 = palanca2.localRotation.eulerAngles.x;
            giro2 = val2.Remap(300, 360, 1, 0.5f);
        }
        else if (palanca2.localRotation.eulerAngles.x >= 0 && palanca2.localRotation.eulerAngles.x <= 60)
        {
            val2 = palanca2.localRotation.eulerAngles.x;
            giro2 = val2.Remap(0, 60, 0.5f, 0);
        }
        palanca2.localRotation = Quaternion.Euler(val2, 180, 0);


        if (handle1.isSelected && handle2.isSelected)
        {
            entroOffset = true;

            door.localEulerAngles = new Vector3(0, 0, offsetDoor + ((giro1 * multiDoor) + (giro2 * multiDoor)));
        }
        else
        {
            if (entroOffset)
            {
                offsetDoor += (giro1 * multiDoor) + (giro2 * multiDoor);
                entroOffset = false;
            }
            //door.localEulerAngles = new Vector3(0, 0, (giro1 * multiDoor) + (giro2 * multiDoor));
        }

        if (door.localEulerAngles.z >= 90)
        {
            teleporter.canTeleport = true;
        }
    }
}