using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenController : MonoBehaviour
{
    public GameObject OVRCameraRig;
    public TMP_Text screenText;
    bool isIndexFingerPinching;
    float ringFingerPinchStrength;
    OVRHand hand;

    private TouchScreenKeyboard overlayKeyboard;
    public static string inputText = "";
    // Start is called before the first frame update
    void Start()
    {
        //hand = GetComponent<OVRHand>();
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
            //overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            if (overlayKeyboard != null)
        {
            inputText = overlayKeyboard.text;
            screenText.text = inputText;
        }*/

        //isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        //ringFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);

        screenText.text = screenText.text + inputText;
        inputText = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
    public static void PrintKey(char key)
    {
        inputText = inputText + key;
    }
}
