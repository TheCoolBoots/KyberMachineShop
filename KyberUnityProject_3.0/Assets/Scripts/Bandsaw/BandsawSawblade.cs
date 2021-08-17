using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandsawSawblade : MonoBehaviour
{
    public bool bandsawOn = false;

    public void TurnBandsawOn()
    {
        bandsawOn = true;
    }
     
    public void TurnBandsawOff()
    {
        bandsawOn = false;
    }
}
