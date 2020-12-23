using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class PaintTool : MonoBehaviour
{
    [SerializeField]
    private GameObject Painting;    //Prefab mit dem Line-Renderer
    [SerializeField]
    private OVRHand handL;          //Linke Oculus-Hand
    [SerializeField]
    private OVRHand handR;          //Rechte Oculus-Hand
    [SerializeField]
    private float pinchThreshold = 0.9f;    //Wert ab wann die Pinch-Geste auch als solche anerkannt wird

    bool alreadyPainting;   //ob im letzten Frame schon gemalt wurde
    bool lastPinchLeft;     //ob im letzten Frame die linke Hand gepinched hat 
    bool lastPinchRight;    //ob im letzten Frame die rechte Hand gepinched hat 
    LineRenderer currentPainting;   //LineRenderer des aktuellen Gemalten
    GameObject boneL;       //Spitze des linken Zeigefingers (Startpunkt des Gemalten)
    GameObject boneR;       //Spitze des linken Zeigefingers (Startpunkt des Gemalten)

    public static bool isPainting;      //ob der Malen-Knopf gedrückt wurde
    public static PaintTool Instance;   //Aktuelle Instance des Paint-Tools (Um es mit den Buttens am Tisch erreichen zu können)
    //Awake wird beim Aktivieren des Skripts aufgerufen
    void Awake()
    {
        //Static Instance wird auf aktuelles Objekt gesetzt
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //wenn malen aktiviert ist
        if (isPainting)
        {
            //Checke von welcher Hand gemalt wird (wenn beide pinchen, wird die gewählt, die auch vorher gemalt hat)
            if (CheckPinch(true) && !lastPinchRight)
            {
                DrawShape(alreadyPainting, boneL.transform.position);
                lastPinchLeft = true;
                alreadyPainting = true;
            }
            else if (CheckPinch(false) && !lastPinchLeft)
            {
                DrawShape(alreadyPainting, boneR.transform.position);
                lastPinchRight = true;
                alreadyPainting = true;
            }
            else
            {
                lastPinchRight = false;
                lastPinchLeft = false;
                alreadyPainting = false;
            }
        }
    }
    //Check which hand is pinching
    private bool CheckPinch(bool leftHand)
    {
        //get PinchStrength from correct hand
        float pinchStrength;
        if (leftHand) {
            
            pinchStrength = handL.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        }
        else
        {
            pinchStrength = handR.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        }
        //Check if detected pinch gesture is strong enough
        if(pinchStrength >= pinchThreshold)
        {
            //find fingertips if not already found (OVRHands get their bones when a hand is first detected
            if (leftHand)
            {
                if (boneL == null)
                {
                    boneL = handL.gameObject.transform.Find("Bones").transform.GetChild(0).transform.Find("Hand_Index1").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                }
            }
            else
            {
                if (boneR == null)
                {
                    boneR = handR.gameObject.transform.Find("Bones").transform.GetChild(0).transform.Find("Hand_Index1").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }
    //Malt die Linie
    private void DrawShape(bool alreadyPaint, Vector3 point)
    {
        //Checkt ob ein neues Painting angefangen wurde, oder ein aktuelles fortgesetzt wird
        if (!alreadyPaint)
        {
            //start new Painting
            //Erstellt ein neues Painting-Objekt mit dem PaintTool als Parent
            GameObject currentPaintingObject = Instantiate(Painting, gameObject.transform);
            //Setzt den LineRenderer des Paintings als currentPainting
            currentPainting = currentPaintingObject.GetComponent<LineRenderer>();
            //fügt ein neuen Point zur Liste des LineRenderers hinzu
            currentPainting.positionCount= currentPainting.positionCount + 1;
            currentPainting.SetPosition(currentPainting.positionCount-1, point);
        }
        else
        {
            //continue current paintig
            //fügt ein neuen Point zur Liste des LineRenderers hinzu
            currentPainting.positionCount = currentPainting.positionCount + 1;
            currentPainting.SetPosition(currentPainting.positionCount-1, point);
            //currentPainting.Simplify(0.01f);
        }
    }
    //Wechsel malen und nicht-malen
    public void StartPaint()
    {
        isPainting = !isPainting;
    }
    //Löscht alle Painting Objekte (alle Children)
    public void DeleteAllPaint()
    {
        isPainting = false;
        for(int i = gameObject.transform.childCount-1; i>=0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
