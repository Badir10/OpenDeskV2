using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    public static PositionController Instance;

    [SerializeField]
    private GameObject point;
    [SerializeField]
    private GameObject tablePlane;
    private List<GameObject> pointList = new List<GameObject>();
    private bool pointSetter = true;

    private Vector3 downLeft;
    private Vector3 downRight;
    // public List<GameObject> buttonAnchors = new List<GameObject>();


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
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)  && pointSetter)
        {
            // DebugOculus.Instance.Log(rightController.transform.position.ToString());
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            GameObject pp = Instantiate(point, position, Quaternion.identity);
            pointList.Add(pp);

            // GameObject buttonAnchor = Instantiate(new GameObject("ButtonAnchor"), position, Quaternion.identity);
            // buttonAnchors.Add(buttonAnchor);
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && pointSetter)
        {
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            GameObject pp = Instantiate(point, position, Quaternion.identity);
            pointList.Add(pp);
            
            // GameObject buttonAnchor = Instantiate(new GameObject("ButtonAnchor"), position, Quaternion.identity);
            // buttonAnchors.Add(buttonAnchor);
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
            foreach(GameObject point in pointList){
                GameObject.Destroy(point);
            }
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

        GameObject scaledTable = tp.transform.GetChild(0).gameObject;
        float legScaleY = tp.transform.localPosition.y;
        float legScaleXZ = 0.05f;
        Vector3 topRight = new Vector3(scaledTable.transform.localScale.x - legScaleXZ, -(tp.transform.position.y), scaledTable.transform.localScale.z - legScaleXZ) / 2;
        Vector3 topLeft = new Vector3(-(scaledTable.transform.localScale.x) + legScaleXZ, -(tp.transform.position.y), scaledTable.transform.localScale.z - legScaleXZ) / 2;
        downRight = new Vector3(scaledTable.transform.localScale.x - legScaleXZ, -(tp.transform.position.y), -(scaledTable.transform.localScale.z) + legScaleXZ) / 2;
        downLeft = new Vector3(-(scaledTable.transform.localScale.x) + legScaleXZ, -(tp.transform.position.y), -(scaledTable.transform.localScale.z) + legScaleXZ) / 2;
        Vector3[] legPosition = new[] {topRight, topLeft, downRight, downLeft};
        MeshRenderer scaledTableMesh = scaledTable.GetComponent<MeshRenderer>();

        foreach(Vector3 legpos in legPosition){
            GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leg.transform.localEulerAngles = tp.transform.localEulerAngles;
            leg.transform.position = tp.transform.position;
            leg.transform.localScale = new Vector3(legScaleXZ, legScaleY, legScaleXZ);
            leg.transform.SetParent(tp.transform);
            leg.transform.localPosition = legpos;
            MeshRenderer legMesh = leg.GetComponent<MeshRenderer>();
            legMesh.material = scaledTableMesh.material;
        }
    }

    public Vector3 getLeftPos(){
        return downLeft;
    }

    public Vector3 getRightPos(){
        return downRight;
    }
}