using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressTopAssembly : MonoBehaviour
{
    [Header("Class Fields")]
    [SerializeField] private RockerSwitch powerSwitch;
    [SerializeField] private FloatKnob heightKnob;
    [SerializeField] private AudioSource windUpDrill;
    [SerializeField] private AudioSource continuousDrill;
    [SerializeField] private AudioSource windDownDrill;

    [Header("Settings")]
    [SerializeField] private float travelDistance = 1f;

    private float startYPos;

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, startYPos - heightKnob.outputVal * travelDistance, transform.position.z);
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
}
