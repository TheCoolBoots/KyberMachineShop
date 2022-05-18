using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TutorialPart
{
    [SerializeField] private UnityEvent startEvents;
    [SerializeField] private UnityEvent endEvents;
    public bool complete = false;

    public void invokeStartEvents()
    {
        startEvents.Invoke();
    }

    public void invokeEndEvents()
    {
        complete = true;
        endEvents.Invoke();
    }
}
