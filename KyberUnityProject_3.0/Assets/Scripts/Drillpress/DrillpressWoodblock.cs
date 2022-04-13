using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressWoodblock : MonoBehaviour
{
    [SerializeField] private GameObject topCap;
    [SerializeField] private GameObject botCap;
    [SerializeField] private ParticleSystem topSystem;
    [SerializeField] private ParticleSystem botSystem;
    [SerializeField] private Collider drillbitCollider;
    [SerializeField] private float collisionThreshold = .1f;


    private bool drilledThrough = false;

    // Update is called once per frame
    void Update()
    {
        if(!drilledThrough && (topCap.transform.localPosition.z - botCap.transform.localPosition.z < collisionThreshold))
        {
            topCap.SetActive(false);
            botCap.SetActive(false);
            drilledThrough = true;
            topSystem.Stop();
            botSystem.Stop();
        }
    }

    public void SetupForDrillpress()
    {
        topCap.GetComponent<Rigidbody>().isKinematic = false;
        botCap.GetComponent<Rigidbody>().isKinematic = false;
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), drillbitCollider, true);
    }

    public void ResetWoodblock()
    {
        topCap.GetComponent<Rigidbody>().isKinematic = true;
        botCap.GetComponent<Rigidbody>().isKinematic = true;
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), drillbitCollider, false);
    }
}
