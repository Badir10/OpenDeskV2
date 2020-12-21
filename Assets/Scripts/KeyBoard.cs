using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBoard : MonoBehaviour
{
    public string Keycode1;
    public string Keycode2;
    public string Keycode3;
    private static bool shift;
    private static bool altGr;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;

    bool pause;
    private bool pressed = false;
    Renderer rend;
    private void Start()
    {

        shift = false;
        rend = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        text1.text = Keycode1;
        text2.text = Keycode2;
        text3.text = Keycode3;
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "; " + other.tag);
        if (other.name.Equals("Hand_Index2_CapsuleCollider") || other.name.Equals("Hand_Index3_CapsuleCollider"))
        {
            if (Keycode1.Equals("Shift")){
                shift = !shift;
                rend.material.color = Color.gray;
            }else if (Keycode1.Equals("alt gr"))
            {
                altGr = !altGr;
                rend.material.color = Color.gray;
            }
            else
            {
                if (!pressed)
                {
                    rend.material.color = Color.gray;

                    pressed = true;
                    if (Keycode1.Equals("Del"))
                    {
                        ScreenController.DeleteKey();
                    }
                    else if (altGr)
                    {
                        if(Keycode3.Length > 0)
                        {
                            ScreenController.PrintKey(Keycode3);
                        }                        
                    }
                    else if (shift)
                    {
                        if (Keycode2.Length > 0)
                        {
                            ScreenController.PrintKey(Keycode2);
                        }
                        else
                        {
                            ScreenController.PrintKey(Keycode1);
                        }
                    }
                    else
                    {
                        ScreenController.PrintKey(Keycode1.ToLower());
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("Hand_Index2_CapsuleCollider") || other.name.Equals("Hand_Index3_CapsuleCollider"))
        {
            pressed = false;
            if (Keycode1.Equals("Shift"))
            {
                if (shift)
                {
                    rend.material.color = new Color(0f, 0f, 0f, 1);
                }
                else
                {
                    rend.material.color = Color.white;
                }
            }
            else if (Keycode1.Equals("alt gr"))
            {
                if (altGr)
                {
                    rend.material.color = new Color(0f, 0f, 0f, 1);
                }
                else
                {
                    rend.material.color = Color.white;
                }
            }
            else
            {
                rend.material.color = Color.white;
            }
        }
    }
}
