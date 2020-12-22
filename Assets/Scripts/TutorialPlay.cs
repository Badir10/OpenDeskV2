using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlay : MonoBehaviour
{

    private bool pointSetter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PositionController.Instance.PointSetter == true)
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }

        //animator.Play(1);
    }
}
