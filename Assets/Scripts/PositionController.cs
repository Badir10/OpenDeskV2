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
        Plane test = new Plane();
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
        if(pointList.Count == 3 && pointSetter){
            // Create Table
            pointSetter = false;
            GameObject pointA = pointList[0];
            GameObject pointB = pointList[1];
            GameObject pointC = pointList[2];
            // Calculate rectangle
            CreateRectangle(pointA, pointB, pointC);
        } 


        
        // if (OVRInput.GetDown(OVRInput.PrimaryHandTrigger))
        // {
        //     gameObject.transform.position = leftController.transform.position;
        //     gameObject.transform.position -= new Vector3(0, 0.07f, 0);
        // }
    }

    void CreateRectangle(GameObject posA, GameObject posB, GameObject posC){
        // float x0 = posA.transform.localPosition.x; float y0 = posA.transform.localPosition.y; float z0 = posA.transform.localPosition.z;
        // float x1 = posB.transform.localPosition.x; float y1 = posB.transform.localPosition.y; float z1 = posB.transform.localPosition.z;
        // float x2 = posC.transform.localPosition.x; float y2 = posC.transform.localPosition.y;  float z2 = posC.transform.localPosition.z;

        // float ux = x1-x0; float uy = y1-y0; float uz = z1-z0;
        // float vx = x2-x0; float vy = y2-y0; float vz = z2-z0;

        // Vector3 u_cross_v = new Vector3(uy*vz-uz*vy, uz*vx-ux*vz, ux*vy-uy*vx);


        // Clockwise
        Vector3 p0 = posA.transform.localPosition;
        Vector3 p1 = posB.transform.localPosition;
        Vector3 p2 = posC.transform.localPosition;

        Vector3 v0 = p0 - p1;
        Vector3 v1 = p2 - p1;
        Vector3 center = p0 + (p2 - p0) / 2;
        Vector3 u0 = new Vector3(1,0,0);

        float scalar = Vector3.Dot(v0, u0);
        float lengthv0 = Vector3.Magnitude(v0);
        float lengthv1 = Vector3.Magnitude(v1);
        float lengthu0 = Vector3.Magnitude(u0);
        float phi = Mathf.Acos(scalar/(lengthu0*lengthv0));

        tp = Instantiate(tablePlane, center, Quaternion.Euler(0,phi,0));
        tp.transform.localScale = new Vector3(lengthv0/2, 0.01f, lengthv1/2);

        // float xCenter = (posA.transform.localPosition.x + posB.transform.localPosition.x)/2;
        // float yCenter = (posA.transform.localPosition.y + posB.transform.localPosition.y)/2;
        // float zCenter = (posA.transform.localPosition.z - posB.transform.localPosition.z)/2;
        // float xScale = Mathf.Abs(posA.transform.localPosition.x - posB.transform.localPosition.x);
        // float zScale = Mathf.Abs(posA.transform.localPosition.z - posB.transform.localPosition.z);
        // Vector3 planeScale = new Vector3(xScale, 1, zScale);
        // Vector3 planePos = new Vector3(xCenter, yCenter, zCenter);
        // tp = Instantiate(tablePlane, planePos, Quaternion.identity);
        // tp.transform.localScale = planeScale;
        // DebugOculus.Instance.Log(tp.transform.localScale.ToString());

        // Plane plane = new Plane();
        // plane.Set3Points(posA.transform.localPosition, posB.transform.localPosition, posC.transform.localPosition);
        // Vector3 planeNormal = plane.normal;
        // DebugOculus.Instance.Log("X: " + plane.normal.x.ToString() + " Y: " + plane.normal.y.ToString() + " Z: " + plane.normal.z.ToString());
    }
}
