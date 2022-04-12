using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float gripMulti = 1;
    [SerializeField] private float pointerMulti = 1;
    [SerializeField] private float teleportMulti = 1;

    private ActionBasedController controller;
    private Animator anim;

    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky),
        new Finger(FingerType.Index)
    };

    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb)
    };

    private readonly List<Finger> teleportFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky),
        new Finger(FingerType.Thumb)
    };

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        controller = transform.parent.parent.GetComponent<ActionBasedController>();
    }

    private void Update()
    {
        //Store input
        CheckGrip();
        CheckPointer();

        //Smooth input values
        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        //Apply smoothed values
        AnimateFinger(pointFingers);
        AnimateFinger(gripFingers);
    }

    private void CheckGrip()
    {
        SetFingerTargets(gripFingers, controller.selectActionValue.action.ReadValue<float>() * gripMulti);
    }

    private void CheckPointer()
    {
        SetFingerTargets(pointFingers, controller.activateActionValue.action.ReadValue<float>() * pointerMulti);
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach(Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach(Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach(Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        anim.SetFloat(finger, blend);
    }
}