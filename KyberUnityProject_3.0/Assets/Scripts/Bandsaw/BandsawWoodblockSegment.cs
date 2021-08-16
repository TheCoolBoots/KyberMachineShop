using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class BandsawWoodblockSegment : MonoBehaviour
{
    [SerializeField] private BandsawWoodblockSegment nextSegment = null;
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Rigidbody segmentRigidbody;
    [SerializeField] private BoxCollider segmentCollider;

    [Space]
    [SerializeField] private GameObject parentContainer;
    [SerializeField] private GameObject rightBlock;
    [SerializeField] private GameObject leftBlock;

    private float startXPos;
    private float linearLimit;

    // Start is called before the first frame update
    private void Start()
    {
        startXPos = transform.position.x;

        linearLimit = segmentCollider.bounds.extents.x;

        SoftJointLimit newLimits = joint.linearLimit;
        newLimits.limit = linearLimit * 1.001f; // give limit a .1% buffer as to not agitate parent rigidbody
        joint.linearLimit = newLimits;

        if (nextSegment = null)
            joint.xMotion = ConfigurableJointMotion.Locked;

        if (segmentRigidbody == null)
            segmentRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Mathf.Abs(transform.position.x - startXPos) >= linearLimit)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BandsawSawblade") && collision.gameObject.GetComponent<BandsawSawblade>().bandsawOn)
        {
            segmentRigidbody.velocity = Vector3.zero;
            segmentRigidbody.isKinematic = false;
        }
        else if(collision.gameObject.CompareTag("BandsawSawblade") && collision.gameObject.GetComponent<BandsawSawblade>().bandsawOn && nextSegment == null)
        {
            rightBlock.transform.SetParent(null, true);
            rightBlock.GetComponent<Interactable>().enabled = true;
            leftBlock.transform.SetParent(null, true);
            leftBlock.GetComponent<Interactable>().enabled = true;
            parentContainer.GetComponent<Interactable>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        segmentRigidbody.velocity = Vector3.zero;
        segmentRigidbody.isKinematic = true;
    }
}
