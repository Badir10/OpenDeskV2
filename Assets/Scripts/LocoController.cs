using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
public class LocoController : MonoBehaviour
{ 
    public VRTeleporter teleporter;
    public OVRHand hand;

    private void Start()
    {
        
    }

    void Update () {

        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            teleporter.ToggleDisplay(true);
            Debug.Log("Nicht Teleportieren");
        }

        if(OVRInput.GetUp(OVRInput.Button.Two))
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
            Debug.Log("Teleportieren");
        }

        if (hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        {
            teleporter.ToggleDisplay(true);
            Debug.Log("Nicht Teleportieren");
        }

        if (!hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        {
            teleporter.Teleport();
            teleporter.ToggleDisplay(false);
            Debug.Log("Teleportieren");
        }
        
    }
}
