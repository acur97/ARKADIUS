using UnityEngine;

public class MoveByFlick : MonoBehaviour
{
    [SerializeField] private float flickSpeed = -0.002f;
    [SerializeField] private RotationConstraintEuler rotationEuler;
    [SerializeField] Transform grab;

    [Space]
    [SerializeField] Transform move;
    [SerializeField] Transform root;

    private bool select = false;
    private float newZ = 0;
    private float newZ2 = 0;

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
    }

    private void Update()
    {
        if (select)
        {
            move.localPosition = new Vector3(move.localPosition.x, move.localPosition.y, newZ + (rotationEuler.totalRotation * flickSpeed));
            root.localPosition = new Vector3(move.localPosition.x, move.localPosition.y, newZ2 + (rotationEuler.totalRotation * flickSpeed));
        }
    }
}