using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class RockerSwitch : MonoBehaviour
{
    [SerializeField] private HingeJoint switchHingeJoint;
    [SerializeField] private Rigidbody switchRigidbody;
    [SerializeField] private AudioSource onSoundEffect;
    [SerializeField] private AudioSource offSoundEffect;


    [Space]
    [SerializeField] private UnityEvent switchOnEvents;
    [SerializeField] private UnityEvent switchOffEvents;

    public bool on = false;

    private bool lastState = false;

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
        {
            onSoundEffect.Play();
            switchOnEvents.Invoke();
        }
            
        else if (!on && lastState)
        {
            offSoundEffect.Play();
            switchOffEvents.Invoke();
        }
            

        lastState = on;
    }
}
