using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlay : MonoBehaviour
{

    private bool pointSetter;
  

    // Update is called once per frame
    void Update()
    {
        if (PositionController.Instance.BuildState)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
