using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressWoodblockSquish : MonoBehaviour
{
    [SerializeField] private string drillpressLabel;
    [SerializeField] private BoxCollider woodblockSquishTrigger;
 

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(drillpressLabel))
        {
            Ray ray = new Ray(transform.position, Vector3.forward);

            woodblockSquishTrigger.Raycast(ray, out RaycastHit hitInfo, 10);

            // drillpress woodblock hole is along z local axis, y global axis
            Debug.Log($"distance: {hitInfo.distance}, worldspace hit point: {hitInfo.point}");
            float dh = woodblockSquishTrigger.bounds.extents.z - hitInfo.distance;

            dh = .1f;

            transform.position = new Vector3(transform.position.x, transform.position.y - dh / 2, transform.position.z);

            //transform.localPosition = new Vector3(0, 0, transform.localPosition.z);

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - transform.localScale.z * dh / (2 * woodblockSquishTrigger.bounds.extents.z));


        }
    }
}