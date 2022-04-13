using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodblockCaps : MonoBehaviour
{
    [SerializeField] private string drillbitTag;
    [SerializeField] private GameObject drillParticleSystem;

    private bool particleSystemActive = false;

    private float localXPos;
    private float localYPos;

    private void Start()
    {
        localXPos = transform.localPosition.x;
        localYPos = transform.localPosition.y;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(localXPos, localYPos, transform.localPosition.z);
    }

    private void OnCollisionEnter(Collision collision)
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
