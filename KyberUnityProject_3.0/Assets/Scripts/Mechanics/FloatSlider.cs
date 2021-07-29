
using UnityEngine;
using TMPro;

public class FloatSlider : MonoBehaviour
{
    [SerializeField] private TextMeshPro outputLabel;
    [SerializeField] private ConfigurableJoint sliderConfigurableJoint;
    [SerializeField] private Vector2 sliderRange = new Vector2(-1f, 1f);

    [Space]
    public float outputVal = 0f;

    private float startZPos;
    private float linearLimit;

    // Start is called before the first frame update
    void Start()
    {
        if (sliderConfigurableJoint == null)
        {
            sliderConfigurableJoint = GetComponent<ConfigurableJoint>();
            if (sliderConfigurableJoint == null)
                Debug.LogError($"sliderConfigurableJoint not assigned in editor and no ConfigurableJoint within { gameObject.name }");
        }
        else
        {
            startZPos = transform.localPosition.z;
            linearLimit = sliderConfigurableJoint.linearLimit.limit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        outputVal = map(transform.localPosition.z, startZPos, startZPos + 2 * linearLimit, sliderRange.x, sliderRange.y);
        outputLabel.text = outputVal.ToString("F3");
    }

    // Maps a value relative to a1 and b1 to a value relative to a2 and b2")
    // same thing as Arduino's Map()
    private float map(float val, float a1, float b1, float a2, float b2)
    {
        return (b2 - a2) * ((val - a1) / (b1 - a1)) + a2; 
    }
}
