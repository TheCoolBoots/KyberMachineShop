using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressTopAssembly : MonoBehaviour
{
    [Header("Class Fields")]
    [SerializeField] private RockerSwitch powerSwitch;
    [SerializeField] private AudioSource windUpDrill;
    [SerializeField] private AudioSource continuousDrill;
    [SerializeField] private AudioSource windDownDrill;

    [Header("Drill Engager Knob")]
    [SerializeField] private FloatKnob heightKnob;
    [SerializeField] private Transform heightKnobTransform;
    [SerializeField] private HingeJoint heightKnobHinge;

    [Header("Settings")]
    [SerializeField] private float travelDistance = 1f;

    private float startYPos;
    private bool denyMovement;
    private float lastKnobOutputVal;

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void Update()
    {
        if (!denyMovement)
            transform.position = new Vector3(transform.position.x, startYPos - heightKnob.outputVal * travelDistance, transform.position.z);
        else if (lastKnobOutputVal > heightKnob.outputVal+.001)
            OnEnableDownMovement();

        lastKnobOutputVal = heightKnob.outputVal;
            
    }

    public void OnDrillStart()
    {
        windUpDrill.Play();
        windDownDrill.Pause();
        continuousDrill.Play();
    }
    
    public void OnDrillStop()
    {
        windDownDrill.Play();
        continuousDrill.Pause();
    }

    public void OnDenyDownMovement()
    {
        JointLimits limits = heightKnobHinge.limits;
        limits.min = heightKnobTransform.localEulerAngles.y - 360;
        heightKnobHinge.limits = limits;
        heightKnobHinge.useLimits = true;

        denyMovement = true;
    }

    public void OnEnableDownMovement()
    {
        JointLimits limits = heightKnobHinge.limits;
        limits.min = -180;
        heightKnobHinge.limits = limits;
        heightKnobHinge.useLimits = true;

        denyMovement = false;
    }
}
