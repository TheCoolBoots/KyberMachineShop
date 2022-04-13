using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class BandsawWoodblockSegment : MonoBehaviour
{
    [SerializeField] private bool lastSegment = false;
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Rigidbody segmentRigidbody;
    [SerializeField] private BoxCollider segmentCollider;

    [Space]
    [SerializeField] private GameObject parentContainer;
    [SerializeField] private GameObject rightBlock;
    [SerializeField] private GameObject leftBlock;

    private float startXPos;
    private float linearLimit;


    /**
     * each segment must begin kinematic to ensure that it moves correctly with the parent
     * when the bandsaw collides with the trigger box collider, make the segment non-kinematic 
     *      which allows for collisions between kinematic sawblade and non-kinematic segment
     *      then set its configurable joint to lock all movement except in the X direction
     *      linear limit and connected anchor are automatically configured
     *      if the joint begins in the locked configuration, the object does not react to physics correctly
     * when the trigger exits, put the segment back into kinematic and free all movement of the joint
     * 
     * if the segment is the last one in the wood, the configuration of the joint does not matter
     * keep the segment as kinematic
     * when the sawblade collides with the last segment, set the parent of the left and right subsegments to the world
     *      activate each of their interactibles and colliders so players can interact with them
     *      finally, disable the collider and interactible from the parent effectively taking it out of the scene
     *      
     * on every frame, check if the local position of the segment has traveled the extent of its length
     *      if it has, destroy it
     * 
     */


    // Start is called before the first frame update
    private void Start()
    {
        if (segmentRigidbody == null)
            segmentRigidbody = GetComponent<Rigidbody>();
        if (segmentCollider == null)
            segmentCollider = GetComponent<BoxCollider>();
        if (joint == null)
            joint = GetComponent<ConfigurableJoint>();

        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = new Vector3(transform.localPosition.x - transform.localScale.x / 2, joint.connectedAnchor.y, joint.connectedAnchor.z);

        linearLimit = segmentCollider.bounds.extents.x;

        SoftJointLimit newLimits = joint.linearLimit;
        newLimits.limit = linearLimit * 1.01f; // give limit a .1% buffer as to not agitate parent rigidbody
        joint.linearLimit = newLimits;

        startXPos = transform.localPosition.x;

    }

    // Update is called once per frame
    private void Update()
    {
        if(Mathf.Abs(transform.localPosition.x - startXPos) >= transform.localScale.x)
        {
            Debug.Log($"transform.position.x: {transform.position.x}, startXPos: {startXPos}, linearLimit: {linearLimit}");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BandsawSawblade"))
        {
            if (other.gameObject.GetComponent<BandsawSawblade>().bandsawOn)
            {
                segmentRigidbody.velocity = Vector3.zero;
                segmentRigidbody.isKinematic = false;

                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Locked;
                joint.zMotion = ConfigurableJointMotion.Locked;
                joint.angularXMotion = ConfigurableJointMotion.Locked;
                joint.angularYMotion = ConfigurableJointMotion.Locked;
                joint.angularZMotion = ConfigurableJointMotion.Locked;
                Debug.Log("Segment set to non-kinematic and joint is limiting X movement");

                if (lastSegment)
                {


                    rightBlock.transform.SetParent(null, true);
                    rightBlock.GetComponent<Interactable>().enabled = true;
                    rightBlock.GetComponent<Collider>().enabled = true;
                    rightBlock.GetComponent<Rigidbody>().useGravity = true;
                    rightBlock.GetComponent<Rigidbody>().isKinematic = false;


                    leftBlock.transform.SetParent(null, true);
                    leftBlock.GetComponent<Interactable>().enabled = true;
                    leftBlock.GetComponent<Collider>().enabled = true;
                    leftBlock.GetComponent<Rigidbody>().useGravity = true;
                    leftBlock.GetComponent<Rigidbody>().isKinematic = false;

                    parentContainer.GetComponent<Interactable>().enabled = false;
                    parentContainer.GetComponent<Collider>().enabled = false;
                    Debug.Log("Releasing sub-woodblocks");
                    Destroy(gameObject);
                }
            }
            else
            {
                Physics.IgnoreCollision(other, parentContainer.GetComponent<Collider>(), false);
                Debug.Log("allowing collisions with parent");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BandsawSawblade"))
        {
            segmentRigidbody.velocity = Vector3.zero;
            segmentRigidbody.isKinematic = true;
            joint.xMotion = ConfigurableJointMotion.Free;
            joint.yMotion = ConfigurableJointMotion.Free;
            joint.zMotion = ConfigurableJointMotion.Free;
            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Free;
            // segmentRigidbody.constraints = RigidbodyConstraints.None;
            Physics.IgnoreCollision(other, parentContainer.GetComponent<Collider>(), true);
        }
    }
}
