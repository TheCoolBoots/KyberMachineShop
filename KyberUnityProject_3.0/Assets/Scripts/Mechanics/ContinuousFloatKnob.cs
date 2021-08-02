using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinuousFloatKnob : MonoBehaviour
{
    public float actualAngle = 0;
    [SerializeField] private TextMeshPro outputLabel;

    private float lastYRotation;
    private float currentYRotation;
    private int numRotations;

    void Start()
    {
        lastYRotation = transform.localEulerAngles.y;
        currentYRotation = lastYRotation;
        numRotations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentYRotation = transform.localEulerAngles.y;

        if (lastYRotation <= 20 && currentYRotation >= 340)
        {
            // angle jumped from 0 to 360
            numRotations -= 1;
        }
        else if (lastYRotation >= 340 && currentYRotation <= 20)
        {
            // angle jumped from 360 to 0
            numRotations += 1;
        }

        actualAngle = numRotations * 360 + currentYRotation;

        lastYRotation = currentYRotation;
        
        outputLabel.text = actualAngle.ToString("F3");
    }

}
