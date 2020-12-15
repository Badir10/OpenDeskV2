using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    public char Key;

    public void PrintKey()
    {
        ScreenController.PrintKey(Key);
    }
}
