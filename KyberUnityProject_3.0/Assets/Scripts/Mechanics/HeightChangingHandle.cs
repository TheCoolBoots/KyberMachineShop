
using UnityEngine;
using Valve.VR.InteractionSystem;

public class HeightChangingHandle : MonoBehaviour
{
    [SerializeField] private Rigidbody connectedTableRigidbody;
    [SerializeField] private FixedJoint jointBetweenTableAndHandle;
    void Start()
    {
        if (connectedTableRigidbody == null)
            Debug.LogError($"{gameObject.name} not assigned a connected body");
        else if (jointBetweenTableAndHandle == null)
        {
            jointBetweenTableAndHandle = GetComponent<FixedJoint>();
            if (jointBetweenTableAndHandle == null)
                Debug.LogError($"{gameObject.name} configuration failed; needs fixed joint component");
        }
        else
        {
            jointBetweenTableAndHandle.connectedBody = connectedTableRigidbody;
        }
    }

    private void OnAttachedToHand(Hand hand)
    {
        connectedTableRigidbody.isKinematic = false;
    }

    private void OnDetachedFromHand(Hand hand)
    {
        connectedTableRigidbody.isKinematic = true;
    }


}
