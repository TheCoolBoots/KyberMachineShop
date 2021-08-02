using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressTopAssembly : MonoBehaviour
{
    [Header("Class Fields")]
    [SerializeField] private RockerSwitch powerSwitch;
    [SerializeField] private FloatKnob heightKnob;


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
}
