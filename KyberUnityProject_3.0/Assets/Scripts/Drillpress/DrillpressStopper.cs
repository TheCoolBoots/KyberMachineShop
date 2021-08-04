using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrillpressStopper : MonoBehaviour
{
    [SerializeField] private Rigidbody topAssemblyRigidbody;
    [SerializeField] private DrillpressTopAssembly topAssembly;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Should Stop Now");
        topAssembly.OnDenyDownMovement();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Back on");
        //topAssembly.OnEnableDownMovement();
    }

}
