using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tooltip : MonoBehaviour
{

    [SerializeField] private GameObject warningText;
    [SerializeField] private GameObject tooltipText;
    [SerializeField] private int tooltipWaitFrames = 100;

    private int currentFrames = 0;

    private void FixedUpdate()
    {
        if (warningText.activeInHierarchy)
        {
            currentFrames++;
            if(currentFrames > tooltipWaitFrames)
            {
                warningText.SetActive(false);
                tooltipText.SetActive(true);
            }
        }
        else
        {
            currentFrames = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            warningText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            warningText.SetActive(false);
            tooltipText.SetActive(false);
            currentFrames = 0;
        }
    }
}
