using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
     public static PositionController Instance;
    [SerializeField]
    private GameObject rightController;
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject point;
    [SerializeField]
    private GameObject tablePlane;
    private List<GameObject> pointList = new List<GameObject>();
    private bool pointSetter = true;

    private GameObject tp;
    // Start is called before the first frame update
    void Awake(){
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger)  && pointSetter)
        {
            // DebugOculus.Instance.Log(rightController.transform.position.ToString());
            GameObject pp = Instantiate(point, rightController.transform.position, Quaternion.identity);
            pointList.Add(pp);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && pointSetter)
        {
            GameObject pp = Instantiate(point, leftController.transform.position, Quaternion.identity);
            pointList.Add(pp);
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            foreach(GameObject points in pointList){
                GameObject.Destroy(points);
            }
            pointList = new List<GameObject>();
            GameObject.Destroy(tp);
            pointSetter = true;
        }

        // DebugOculus.Instance.Log(pointList.Count.ToString());
        if(pointList.Count == 2 && pointSetter){
            // Create Table
            pointSetter = false;
            GameObject pointA = pointList[0];
            GameObject pointB = pointList[1];
            // Calculate rectangle
            CreateRectangle(pointA, pointB);
        } 


        
        // if (OVRInput.GetDown(OVRInput.PrimaryHandTrigger))
        // {
        //     gameObject.transform.position = leftController.transform.position;
        //     gameObject.transform.position -= new Vector3(0, 0.07f, 0);
        // }
    }

    void CreateRectangle(GameObject posA, GameObject posB){
        float xCenter = (posA.transform.localPosition.x + posB.transform.localPosition.x)/2;
        float yCenter = (posA.transform.localPosition.y + posB.transform.localPosition.y)/2;
        float zCenter = (posA.transform.localPosition.z - posB.transform.localPosition.z)/2;
        float xScale = Mathf.Abs(posA.transform.localPosition.x - posB.transform.localPosition.x);
        float zScale = Mathf.Abs(posA.transform.localPosition.z - posB.transform.localPosition.z);
        Vector3 planeScale = new Vector3(xScale, 1, zScale);
        Vector3 planePos = new Vector3(xCenter, yCenter, zCenter);
        tp = Instantiate(tablePlane, planePos, Quaternion.identity);
        tp.transform.localScale = planeScale;
        DebugOculus.Instance.Log(tp.transform.localScale.ToString());
    }
}
