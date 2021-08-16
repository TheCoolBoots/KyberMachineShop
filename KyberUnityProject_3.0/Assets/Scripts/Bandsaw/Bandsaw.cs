using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandsaw : MonoBehaviour
{
    [SerializeField] private VRSnapPoint snapPoint;

    public void OnWoodblockSnapEngage()
    {
        snapPoint.currentSnappedItem.GetComponent<Rigidbody>().useGravity = false;
    }

    public void OnWoodblockSnapDisengage()
    {
        snapPoint.currentSnappedItem.GetComponent<Rigidbody>().useGravity = true;
    }
}
