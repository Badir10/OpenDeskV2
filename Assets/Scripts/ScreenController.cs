using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private GameObject OVRCameraRig;
    [SerializeField]
    private TMP_Text screenText;    //Das Textfeld auf dem Canvas welchen den Screen auf dem Tisch darstellt
    bool isIndexFingerPinching;
    float ringFingerPinchStrength;
    OVRHand hand;

    static bool deleteKey;      //boolean ob ein Character aus dem Text gelöscht werden soll
    //TouchScreenKeyboard overlayKeyboard;
    static string inputText = "";   //Beinhaltet allen Text, welcher in einem Frame eingegeben wurde

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
        }
        isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        ringFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        */

        //Entfernt den Pointer welcher die aktuelle Schreib-Stelle ("|") anzeigt am Ende des Strings
        screenText.text = screenText.text.Remove(screenText.text.Length - 1);
        //Wenn ein Character aus dem String gelöscht werden soll
        if (deleteKey)
        {
            if(screenText.text.Length > 0)
            {
                //Entfernt den letzten Character aus dem String
                screenText.text = screenText.text.Remove(screenText.text.Length - 1);

                deleteKey = false;
            }
        }
        //Fügt den Pointer welcher die aktuelle Schreib-Stelle ("|") anzeigt und den Text, welcher diesen Frame eingegeben wurde, hinzu
        screenText.text = screenText.text + inputText + "|";
        //Resettet den inputText für den nächsten Frame
        inputText = "";
    }

    //(Veraltet) Detectet ob etwas mit dem Screen kollidiert ist
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
    }
    //(Veraltet) Detectet ob etwas mit dem Screen kollidiert ist und ihn wieder verlassen hat
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
    //Fügt weiteren String dem aktuellen inputText an
    public static void PrintKey(string keycode)
    {
        inputText = inputText + keycode;
    }
    //Aktieviert die Delete Funktion
    public static void DeleteKey()
    {
        deleteKey = true;
    }
}
