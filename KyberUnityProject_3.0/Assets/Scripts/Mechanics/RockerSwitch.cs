﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class RockerSwitch : MonoBehaviour
{
    [SerializeField] private TextMeshPro currentState;
    [SerializeField] private HingeJoint switchHingeJoint;
    [SerializeField] private Rigidbody switchRigidbody;

    [Space]
    [SerializeField] private UnityEvent switchOnEvents;
    [SerializeField] private UnityEvent switchOffEvents;

    public bool on = false;

    private bool lastState = false;

    // Start is called before the first frame update
    void Start()
    {
        if (switchHingeJoint == null)
        {
            switchHingeJoint = GetComponent<HingeJoint>();
            if (switchHingeJoint == null)
                Debug.LogError($"{gameObject.name} does not have hingeJoint component attached to it");
        }
        switchRigidbody.AddRelativeTorque(new Vector3(.02f, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localRotation.eulerAngles.x > 0 && transform.localRotation.eulerAngles.x < 180)
        {
            //Debug.Log("off");
            switchRigidbody.AddRelativeTorque(new Vector3(.02f, 0, 0));
            on = false;
        }
        else
        {
            //Debug.Log("on");
            switchRigidbody.AddRelativeTorque(new Vector3(-.02f, 0, 0));
            on = true;
        }

        if (on && !lastState)
            switchOnEvents.Invoke();
        else if (!on && lastState)
            switchOffEvents.Invoke();

        lastState = on;
    }
}