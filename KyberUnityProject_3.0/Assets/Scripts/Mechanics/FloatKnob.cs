using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatKnob : MonoBehaviour
{
    [SerializeField] private TextMeshPro outputLabel;
    [SerializeField] private Vector2 sliderOutputRange;
    [SerializeField] private HingeJoint knobHingeJoint;

    [Space]
    public float outputVal = 0;

    void Start()
    {
        if(knobHingeJoint == null)
        {
            knobHingeJoint = GetComponent<HingeJoint>();
            if (knobHingeJoint == null)
                Debug.LogError($"{gameObject.name} does not have hingeJoint component attached to it");
        }
    }

    private void Update()
    {
        // Debug.Log(transform.localRotation.eulerAngles.y);
        if(transform.localRotation.eulerAngles.y < 180)
            outputVal = map(transform.localRotation.eulerAngles.y, knobHingeJoint.limits.min, knobHingeJoint.limits.max, sliderOutputRange.x, sliderOutputRange.y);
        else
            outputVal = map(transform.localRotation.eulerAngles.y - 360, knobHingeJoint.limits.min, knobHingeJoint.limits.max, sliderOutputRange.x, sliderOutputRange.y);
        outputLabel.text = outputVal.ToString("F3");
    }

    // Maps a value relative to a1 and b1 to a value relative to a2 and b2")
    // same thing as Arduino's Map()
    private float map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2;
    }
}
