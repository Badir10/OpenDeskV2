using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlay : MonoBehaviour
{
    
    ///// Spielt die Tutorial Animation zu einem bestimmten Zeitpunkt ab /////
    /// 

    // verwendet den Getter der PositionController Klasse um herauszufinden, wann der Tisch gebaut wird
    private bool pointSetter;
  

    
    void Update()
    {
        //Wenn der Tisch gebaut wird soll die Tutorialanimation angeschaltet und danach ausgeschaltet werden
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
