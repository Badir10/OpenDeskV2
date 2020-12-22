using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLimitSpring : MonoBehaviour
{
    public Transform tablePosition;
    public Vector3 buttonPosition;

    public float distanceUp;

    public float distanceDown;

    private void Awake()
    {
        distanceDown = Vector3.Distance(tablePosition.transform.position, transform.position);
        distanceUp = tablePosition.position.y;
        buttonPosition = transform.position;
    }

    void Update()
    {

        if (Vector3.Distance(tablePosition.transform.position, transform.position) >= distanceDown && !PositionController.Instance.tablePosBool)
        {
            transform.position = buttonPosition;
        }

        if (transform.position.y <= distanceUp && !PositionController.Instance.tablePosBool)
        {
            transform.position = new Vector3(transform.position.x, distanceUp, transform.position.z);
        }

        if (transform.position.y != tablePosition.position.y && PositionController.Instance.tablePosBool)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, tablePosition.localPosition.y, transform.localPosition.z);
            distanceDown = Vector3.Distance(tablePosition.transform.position, transform.position);
            distanceUp = tablePosition.position.y;
            buttonPosition = transform.position;
        }
    }
}
