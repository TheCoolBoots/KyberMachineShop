using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressDrillbit : MonoBehaviour
{

    [SerializeField] private DrillpressTopAssembly topAssembly;

    public bool drillbitOn = false;

    public void PowerDrillbitOn()
    {
        drillbitOn = true;
    }

    public void PowerDrillbitOff()
    {
        drillbitOn = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);

        if (!drillbitOn)
        {
            topAssembly.OnDenyDownMovement();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        topAssembly.OnEnableDownMovement();
    }
}
