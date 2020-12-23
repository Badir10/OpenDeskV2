using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
public class LocoController : MonoBehaviour
{ 
    
    ///// diese Klasse sollte einen Teleporter in das Projekt einfuehren. Es hat mit dem Controller funktioniert, aber nicht mit Handtracking /////
    /// deshalb wurde es in der letzten Version nicht verwendet
    ///
    
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
