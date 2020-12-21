using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandGrab : OVRGrabber
{
    private OVRHand hand;
    public float pinchThreshold = 0.7f;
    
    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }
    
    void CheckIndexPinch()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if(!m_grabbedObj && pinchStrength > pinchThreshold && m_grabCandidates.Count > 0)
        {
            GrabBegin();
        }
        else if(m_grabbedObj && ! (pinchStrength > pinchThreshold))
        {
            GrabEnd();
        }
    }
}
