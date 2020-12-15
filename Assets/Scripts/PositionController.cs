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
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)  && pointSetter)
        {
            // DebugOculus.Instance.Log(rightController.transform.position.ToString());
            GameObject pp = Instantiate(point, rightController.transform.position, Quaternion.identity);
            pointList.Add(pp);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && pointSetter)
        {
            GameObject pp = Instantiate(point, leftController.transform.position, Quaternion.identity);
            pointList.Add(pp);
        }

        if (OVRInput.GetDown(OVRInput.Button.Start))
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
        Vector3 p0 = posA.transform.localPosition;
        Vector3 p1 = posB.transform.localPosition;
        Vector3 p2 = posC.transform.localPosition;

        Vector3 v0 = p0 - p1;
        //v0.y = 0;
        Vector3 v1 = p2 - p1;
        //v1.y = 0;
        Vector3 center = p0 + (p2 - p0) / 2;
        center.y = center.y - 0.03f;
        Vector3 u0 = new Vector3(1,0,0);

        float scalar = Vector3.Dot(v0, u0);
        float lengthv0 = Vector3.Magnitude(v0);
        float lengthv1 = Vector3.Magnitude(v1);
        float lengthu0 = Vector3.Magnitude(u0);
        float beta = Mathf.Acos(scalar/(lengthu0*lengthv0));
        beta = beta * 180 / Mathf.PI;
        float alpha = 180- beta;
        Debug.LogError("Winkel a: " + alpha + "; Winkel b: " + beta + "; Vector V0: " + v0.x + ", " + v0.y + ", " + v0.z + "; Scalar: " + scalar);
        float minAngle;
        float maxAngle;
        if (scalar >= 0)
        {
            minAngle = beta;
            maxAngle = alpha;
            if (v0.z >= 0)
            {
                if (v1.x >= 0)
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, maxAngle, 0));
                }
                else
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, -minAngle, 0));
                }
            }
            else
            {
                if (v1.x >= 0)
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, minAngle, 0));
                }
                else
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, -maxAngle, 0));
                }
            }
        }
        else
        {
            minAngle = alpha;
            maxAngle = beta;
            if (v0.z >= 0)
            {
                if (v1.x >= 0)
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, minAngle, 0));
                }
                else
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, -maxAngle, 0));
                }
            }
            else
            {
                if (v1.x >= 0)
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, maxAngle, 0));
                }
                else
                {
                    tp = Instantiate(tablePlane, center, Quaternion.Euler(0, -minAngle, 0));
                }
            }
        }
        tp.transform.GetChild(0).transform.localScale = new Vector3(lengthv0, 0.03f, lengthv1);
    }
}