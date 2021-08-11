using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodblockCaps : MonoBehaviour
{
    [SerializeField] private string drillbitTag;
    [SerializeField] private GameObject drillParticleSystem;

    private bool particleSystemActive = false;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(drillbitTag))
        {
            if (collision.gameObject.GetComponent<DrillpressDrillbit>().drillbitOn)
            {
                particleSystemActive = true;
                drillParticleSystem.SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (particleSystemActive)
        {
            particleSystemActive = false;
            drillParticleSystem.SetActive(false);
        }
            
    }
}
