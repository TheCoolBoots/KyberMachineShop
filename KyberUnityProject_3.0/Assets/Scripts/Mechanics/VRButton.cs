using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VRButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadzone = 0.025f;

    public bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;

    void Start()
    {
        startPos = transform.localPosition;
        if(joint == null)
            joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
            Pressed();
        if (isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / (2 * joint.linearLimit.limit); //  
        var dist = Vector3.Distance(startPos, transform.localPosition);
        // Debug.Log($"Distance: {dist}\t Value: {value}");

        if (Mathf.Abs(value) < deadzone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
