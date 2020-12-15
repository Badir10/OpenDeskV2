using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    public char Key;

    private float timePassed = 0;
    private bool pause = false;
    float maxTime = 0.2f;
    private void Update()
    {
        if (pause)
        {
            timePassed = timePassed + Time.deltaTime;
            if (timePassed >= maxTime)
            {
                timePassed = 0;
                pause = false;
            }
        }
    }

    public void PrintKey()
    {
        if (!pause)
        {
            pause = true;
            ScreenController.PrintKey(Key);
        }
    }
}
