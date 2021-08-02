using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressBottomAssembly : MonoBehaviour
{
    [SerializeField] private ContinuousFloatKnob heightChanger;
    [SerializeField] private Transform woodblockSnapPoint;
    [SerializeField] [Tooltip("the worldspace distance travelled by one rotation of the height changer knob")] private float travelDistance;

    private float startYPos;
    private float outputVal;

    void Start()
    {
        startYPos = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        outputVal = map(heightChanger.actualAngle, 0, 360, 1, 0);
        transform.position = new Vector3(transform.position.x, startYPos - outputVal * travelDistance, transform.position.z);
    }

    private float map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2;
    }
}
