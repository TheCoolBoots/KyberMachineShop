using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillpressWoodblock : MonoBehaviour
{
    [SerializeField] private GameObject topCap;
    [SerializeField] private GameObject botCap;

    private float topCapZStart;
    private float botCapZStart;
    private bool drilledThrough = false;

    // Start is called before the first frame update
    void Start()
    {
        topCapZStart = topCap.transform.localPosition.z;
        botCapZStart = botCap.transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(!drilledThrough && topCap.transform.localPosition.z <= botCap.transform.localPosition.z)
        {
            Destroy(topCap);
            Destroy(botCap);
            drilledThrough = true;
        }
    }
}
