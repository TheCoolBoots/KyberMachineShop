using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FixedJoint))]
public class VRSnapPoint : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;
    [SerializeField] private MeshRenderer colliderBoundsIndicator;
    [SerializeField] private Collider activationThreshold;
    [SerializeField] private Rigidbody snapRigidbody;
    [SerializeField] private FixedJoint fixedJoint;
    [SerializeField] private bool useTagFilter;
    [SerializeField] private List<string> allowedTags;

    [Space]
    [SerializeField] private UnityEvent engageEvents;
    [SerializeField] private UnityEvent disengageEvents;

    [Space]
    public bool snapPointOccipied = false;
    public GameObject currentSnappedItem;



    private void Awake()
    {
        if (snapPoint == null)
            snapPoint = transform;
        if (colliderBoundsIndicator == null)
            colliderBoundsIndicator = GetComponent<MeshRenderer>();
        if (activationThreshold == null)
            activationThreshold = GetComponent<Collider>();
        if (fixedJoint == null)
            fixedJoint = GetComponent<FixedJoint>();
        if (snapRigidbody == null)
            snapRigidbody = GetComponent<Rigidbody>();
        activationThreshold.isTrigger = true;
        colliderBoundsIndicator.enabled = false;

    }


    private void OnTriggerExit(Collider other)
    {
        colliderBoundsIndicator.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (useTagFilter && GameObjectIsOfAllowedType(other.gameObject))
        {
            TriggerStayLogic(other);
        }
        else if (!useTagFilter)
        {
            TriggerStayLogic(other);
        }


    }

    private void TriggerStayLogic(Collider other)
    {
        // if snap point is empty, and component colliding with snap point has a Interactable && Throwable component
        if (!snapPointOccipied && other.GetComponent<Interactable>() != null)
        {
            if (!colliderBoundsIndicator.enabled)
            {
                colliderBoundsIndicator.enabled = true;
            }

            Interactable otherInteractable = other.GetComponent<Interactable>();

            // engage the item to the snap point if it is within the collider bounds and not being grabbed
            if (!otherInteractable.attachedToHand)
            {
                EngageItem(other);
            }
        }

        // disengage the item if there is an item engaged and item is being picked up
        else if (snapPointOccipied && currentSnappedItem.GetComponent<Interactable>().attachedToHand)
        {
            DisengageItem(currentSnappedItem);
        }
    }


    private void EngageItem(Collider other)
    {
        // Debug.Log("Engaging item");
        other.gameObject.transform.position = snapPoint.position;
        other.gameObject.transform.rotation = snapPoint.rotation;
        fixedJoint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
        snapPointOccipied = true;
        currentSnappedItem = other.gameObject;
        colliderBoundsIndicator.enabled = false;
        engageEvents.Invoke();
    }

    private void DisengageItem(GameObject canister)
    {
        fixedJoint.connectedBody = null;
        snapPointOccipied = false;
        disengageEvents.Invoke();
    }

    private void OnDisable()
    {
        if (snapPointOccipied && currentSnappedItem != null)
        {
            DisengageItem(currentSnappedItem);
        }
    }

    private bool GameObjectIsOfAllowedType(GameObject gameObject)
    {
        foreach(string s in allowedTags)
        {
            if (gameObject.tag == s)
                return true;
        }
        return false;
    }

}

