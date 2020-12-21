using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    public KeyCode Keycode;
    private static bool Shift;

    private float timePassed = 0;
    private bool pause = false;
    float maxTime = 0.2f;

    private void Start()
    {
        Shift = false;
    }
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
            if (Keycode == KeyCode.LeftShift || Keycode == KeyCode.RightShift)
                Shift = !Shift;

            pause = true;
            ScreenController.PrintKey(Keycode, Shift, false);
        }
    }
}
