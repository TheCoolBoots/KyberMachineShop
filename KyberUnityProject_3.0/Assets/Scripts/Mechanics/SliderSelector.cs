
using UnityEngine;
using TMPro;

public class SliderSelector : MonoBehaviour
{
    [SerializeField] private TextMeshPro outputLabel;
    [SerializeField] private ConfigurableJoint sliderConfigurableJoint;
    [SerializeField] private int numSegments;
    [SerializeField] private Rigidbody sliderRigidbody;
    [SerializeField] private float returnKP = 1000;

    [Space]
    public int nearestInt = 0;

    private float startZPos;
    private float linearLimit;
    private float floatVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (sliderConfigurableJoint == null)
        {
            sliderConfigurableJoint = GetComponent<ConfigurableJoint>();
            if (sliderConfigurableJoint == null)
                Debug.LogError($"sliderConfigurableJoint not assigned in editor and no ConfigurableJoint within { gameObject.name }");
            //this.enabled = false;
        }
        else if (numSegments <= 0)
        {
            Debug.LogError($"numSegments for { gameObject.name } must be > 0");
            numSegments = 1;
            //  this.enabled = false;
        }
        else
        {
            // initialize variables and output text
            startZPos = transform.localPosition.z;
            linearLimit = sliderConfigurableJoint.linearLimit.limit;
            updateFloatVal();
            outputLabel.text = nearestInt.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateFloatVal();
        // add a force that pushes the slider towards the nearest integer value
        sliderRigidbody.AddForce(0, 0, returnKP * (floatVal - nearestInt));

        // only update the label if slider is outside threshold; for efficiency purposes
        if (Mathf.Clamp(floatVal, nearestInt - .3f, nearestInt + .3f) != floatVal)
        {
            // Debug.Log($"{floatVal} {nearestInt}");
            outputLabel.text = nearestInt.ToString();
        }
    }

    private void updateFloatVal()
    {
        floatVal = map(transform.localPosition.z, startZPos, startZPos + 2 * linearLimit, 0, numSegments - 1);
        nearestInt = Mathf.RoundToInt(floatVal);
    }

    // Maps a value relative to a1 and b1 to a value relative to a2 and b2")
    // same thing as Arduino's Map()
    private float map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2; 
    }


}
