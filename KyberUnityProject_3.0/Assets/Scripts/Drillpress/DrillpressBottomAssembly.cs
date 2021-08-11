using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressBottomAssembly : MonoBehaviour
{
    [SerializeField] private ContinuousFloatKnob heightChanger;
    [SerializeField] [Tooltip("the worldspace distance travelled by one rotation of the height changer knob")] private float travelDistance;
    [SerializeField] private VRSnapPoint woodblockSnapPoint;

    private float startYPos;
    private float outputVal;

    private void Start()
    {
        startYPos = transform.position.y;
    }

    private void Update()
    {
        outputVal = Map(heightChanger.actualAngle, 0, 360, 0, -1);
        transform.position = new Vector3(transform.position.x, startYPos - outputVal * travelDistance, transform.position.z);
    }

    private float Map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2;
    }

    public void SetupIfWoodblock()
    {
        DrillpressWoodblock woodblock;
        if((woodblock = woodblockSnapPoint.currentSnappedItem.GetComponent<DrillpressWoodblock>()) != null)
            woodblock.SetupForDrillpress();
    }

    public void ResetWoodblock()
    {
        DrillpressWoodblock woodblock;
        if ((woodblock = woodblockSnapPoint.currentSnappedItem.GetComponent<DrillpressWoodblock>()) != null)
            woodblock.ResetWoodblock();
    }
}
