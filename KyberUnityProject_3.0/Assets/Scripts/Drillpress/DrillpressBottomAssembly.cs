using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressBottomAssembly : MonoBehaviour
{
    [SerializeField] private ContinuousFloatKnob heightChanger;
    [SerializeField] [Tooltip("the worldspace distance travelled by one rotation of the height changer knob")] private float travelDistance;
    [SerializeField] private GameObject woodblockSnapPointGO;
    [SerializeField] private GameObject clampSnapPointGO;
    [SerializeField] private GameObject useTheClampText;

    private VRSnapPoint woodblockSnapPoint;
    private VRSnapPoint clampSnapPoint;
    private float startYPos;
    private float outputVal;

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void Update()
    {
        woodblockSnapPoint = woodblockSnapPointGO.GetComponent<VRSnapPoint>();
        clampSnapPoint = clampSnapPointGO.GetComponent<VRSnapPoint>();

        outputVal = Map(heightChanger.actualAngle, 0, 360, 0, -1);
        transform.position = new Vector3(transform.position.x, startYPos - outputVal * travelDistance, transform.position.z);

        if (woodblockSnapPoint.snapPointOccipied && !clampSnapPoint.snapPointOccipied)
        {
            useTheClampText.SetActive(true);
        }
        else
        {
            useTheClampText.SetActive(false);
        }
    }

    private float Map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2;
    }

    public void SetupIfWoodblock()
    {
        DrillpressWoodblock woodblock;
        if(woodblockSnapPoint.currentSnappedItem != null && (woodblock = woodblockSnapPoint.currentSnappedItem.GetComponent<DrillpressWoodblock>()) != null)
            woodblock.SetupForDrillpress();
    }

    public void ResetWoodblock()
    {
        DrillpressWoodblock woodblock;
        if (woodblockSnapPoint.currentSnappedItem != null && (woodblock = woodblockSnapPoint.currentSnappedItem.GetComponent<DrillpressWoodblock>()) != null)
            woodblock.ResetWoodblock();
    }
}
