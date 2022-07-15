using UnityEngine;

public class MoveByFlick : MonoBehaviour
{
    [SerializeField] private float zLimit = 10;
    [SerializeField] private float zLimit2 = 10;
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
        root.localPosition = new Vector3(0, root.localPosition.y, Mathf.Clamp(root.localPosition.z, 0, zLimit2));
    }

    private void Update()
    {
        if (select)
        {
            newPos = new Vector3(move.localPosition.x, move.localPosition.y, newZ + (rotationEuler.totalRotation * flickSpeed));
            if (newPos.z > move.localPosition.z)
            {
                move.localPosition = newPos;
                root.localPosition = new Vector3(move.localPosition.x, move.localPosition.y, newZ2 + (rotationEuler.totalRotation * flickSpeed));

                move.localPosition = new Vector3(move.localPosition.x, move.localPosition.y, Mathf.Clamp(move.localPosition.z, 0, zLimit));
                root.localPosition = new Vector3(root.localPosition.x, root.localPosition.y, Mathf.Clamp(root.localPosition.z, 0, zLimit));
            }
        }
    }
}