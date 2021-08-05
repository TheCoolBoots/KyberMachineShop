using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressWoodblock : MonoBehaviour
{
    [SerializeField] private GameObject topCap;
    [SerializeField] private GameObject botCap;
    [SerializeField] private Collider drillbitCollider;

    private bool drilledThrough = false;

    // Update is called once per frame
    void Update()
    {
        if(!drilledThrough && topCap.transform.localPosition.z <= botCap.transform.localPosition.z)
        {
            topCap.SetActive(false);
            botCap.SetActive(false);
            drilledThrough = true;
        }
    }

    public void SetupForDrillpress()
    {
        topCap.GetComponent<Rigidbody>().isKinematic = false;
        botCap.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetWoodblock()
    {
        topCap.GetComponent<Rigidbody>().isKinematic = true;
        botCap.GetComponent<Rigidbody>().isKinematic = true;
    }
}
