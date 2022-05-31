using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandsaw : MonoBehaviour
{
    [SerializeField] private GameObject snapPointGO;
    private VRSnapPoint snapPoint;
    public Vector3 snapPointPos;

    private void Awake()
    {
        snapPoint = snapPointGO.GetComponent<VRSnapPoint>();
        snapPointPos = snapPointGO.transform.localPosition;
    }

    public void OnPowerOn()
    {
        if (snapPoint.snapPointOccipied)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), snapPoint.currentSnappedItem.GetComponent<Collider>(), true);
        }
    }
    public void OnPowerOff()
    {
        Debug.Log("resetting position of snappoint");
        snapPointGO.transform.localPosition = snapPointPos;

    }

}
