using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class PaintTool : MonoBehaviour
{
    [SerializeField]
    private GameObject Painting;
    [SerializeField]
    private OVRHand handL;
    [SerializeField]
    private OVRHand handR;
    [SerializeField]
    private float pinchThreshold = 0.9f;
    public static bool isPainting;
    bool alreadyPainting;
    bool lastPinchLeft;
    bool lastPinchRight;
    LineRenderer currentPainting;

    GameObject boneL;
    GameObject boneR;

    public static PaintTool Instance;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPainting)
        {
            //Check which Hand to draw from
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
        float pinchStrength;
        if (leftHand) {
            
            pinchStrength = handL.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        }
        else
        {
            pinchStrength = handR.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        }
        if(pinchStrength >= pinchThreshold)
        {
            //find fingertips
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
    private void DrawShape(bool alreadyPaint, Vector3 point)
    {
        
        Debug.Log(point.x + ";" + point.y + ";" + point.z);
        if (!alreadyPaint)
        {
            //start new Painting
            GameObject currentPaintingObject = Instantiate(Painting, gameObject.transform);
            currentPainting = currentPaintingObject.GetComponent<LineRenderer>();
            currentPainting.positionCount= currentPainting.positionCount + 1;
            currentPainting.SetPosition(currentPainting.positionCount-1, point);
        }
        else
        {
            //continue current paintig
            currentPainting.positionCount = currentPainting.positionCount + 1;
            currentPainting.SetPosition(currentPainting.positionCount-1, point);
            //currentPainting.Simplify(0.01f);
        }
    }
    public void StartPaint()
    {
        isPainting = !isPainting;
    }
    public void DeleteAllPaint()
    {
        isPainting = false;
        for(int i = gameObject.transform.childCount-1; i>=0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
