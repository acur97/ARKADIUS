using UnityEngine;

public class PullByFlick : MonoBehaviour
{
    [SerializeField] private float zLimit = 34;
    [SerializeField] private float flickSpeed = -0.002f;
    [SerializeField] private RotationConstraintEuler rotationEuler;
    [SerializeField] Transform grab;

    [Space]
    [SerializeField] Transform move;
    [SerializeField] Transform root;

    private bool select = false;
    private float newZ = 0;
    private float newZ2 = 0;
    private Vector3 newPos = Vector3.zero;

    public void OnSelect()
    {
        newZ = move.localPosition.z;
        newZ2 = root.localPosition.z;
        select = true;
        rotationEuler.canUpdate = true;
    }
    public void OnDeselect()
    {
        select = false;

        grab.localEulerAngles = Vector3.zero;
        grab.localPosition = Vector3.zero;
        rotationEuler.canUpdate = false;
        rotationEuler.ResetValues();
        root.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnSelect();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            OnDeselect();
        }

        if (select)
        {
            newPos = new Vector3(move.localPosition.x, move.localPosition.y, newZ - (rotationEuler.totalRotation * flickSpeed));
            if (newPos.z < move.localPosition.z)
            {
                move.localPosition = newPos;

                move.localPosition = new Vector3(move.localPosition.x, move.localPosition.y, Mathf.Clamp(move.localPosition.z, zLimit, 48));
            }
        }
    }
}