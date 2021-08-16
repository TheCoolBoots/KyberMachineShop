using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandsaw : MonoBehaviour
{
    [SerializeField] private VRSnapPoint snapPoint;

    public void OnPowerOn()
    {
        if (snapPoint.snapPointOccipied)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), snapPoint.currentSnappedItem.GetComponent<Collider>(), true);
        }
    }
    public void OnPowerOff()
    {
        if (snapPoint.snapPointOccipied)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), snapPoint.currentSnappedItem.GetComponent<Collider>(), false);
        }
    }

    public void OnWoodblockSnapEngage()
    {
        //snapPoint.currentSnappedItem.GetComponent<Rigidbody>().useGravity = false;
    }

    public void OnWoodblockSnapDisengage()
    {
        //snapPoint.currentSnappedItem.GetComponent<Rigidbody>().useGravity = true;
    }
}
