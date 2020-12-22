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

    float sinceLastClick;
    private bool pressed = false;
    Renderer rend;
    GameObject physicalKey;
    AudioSource click;
    private void Start()
    {

        shift = false;
        rend = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>();
        physicalKey = gameObject.transform.GetChild(0).gameObject;
        click = gameObject.GetComponent<AudioSource>();
        text1.text = Keycode1;
        text2.text = Keycode2;
        text3.text = Keycode3;
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - sinceLastClick > 0.05f)
        {
            Debug.Log(other.name + "; " + other.tag);
            if (other.name.Equals("TargetTransform"))
            {
                if (Keycode1.Equals("Shift"))
                {
                    shift = !shift;
                    rend.material.color = new Color(0.5f, 0.5f, 0.5f, 0.54f);
                }
                else if (Keycode1.Equals("alt gr"))
                {
                    altGr = !altGr;
                    rend.material.color = new Color(0.5f, 0.5f, 0.5f, 0.54f);
                }
                else
                {
                    if (!pressed)
                    {
                        rend.material.color = new Color(0.5f, 0.5f, 0.5f, 0.54f);

                        pressed = true;
                        if (Keycode1.Equals("Del"))
                        {
                            ScreenController.DeleteKey();
                        }
                        else if (Keycode1.Equals("Enter"))
                        {
                            ScreenController.PrintKey("\n");
                        }
                        else if (altGr)
                        {
                            if (Keycode3.Length > 0)
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
                sinceLastClick = Time.time;
                physicalKey.transform.localPosition = new Vector3(0, -0.128f, 0);
                click.Stop();
                click.Play();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("TargetTransform"))
        {
            pressed = false;
            if (Keycode1.Equals("Shift"))
            {
                if (shift)
                {
                    rend.material.color = new Color(0f, 0f, 0f, 0.54f);
                }
                else
                {
                    rend.material.color = new Color(1,1,1, 0.54f);
                }
            }
            else if (Keycode1.Equals("alt gr"))
            {
                if (altGr)
                {
                    rend.material.color = new Color(0f, 0f, 0f, 0.54f);
                }
                else
                {
                    rend.material.color = new Color(1, 1, 1, 0.54f);
                }
            }
            else
            {
                rend.material.color = new Color(1, 1, 1, 0.54f);
            }
            physicalKey.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
