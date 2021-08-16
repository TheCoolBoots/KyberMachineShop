using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandsawGuide : MonoBehaviour
{
    [SerializeField] private FloatKnob heightKnob;
    [SerializeField] private ConfigurableJoint joint;

    private float startYPos;
    private float linearDistance;

    private void  Start()
    {
        startYPos = transform.position.y;
        linearDistance = joint.linearLimit.limit;
    }

    private void Update()
    {
        float newHeight = Map(heightKnob.outputVal, -1, 1, startYPos - linearDistance, startYPos + linearDistance);
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }

    private float Map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2;
    }
}
