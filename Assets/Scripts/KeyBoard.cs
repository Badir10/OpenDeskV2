using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Script wird jedem Key der Tastatur angefügt
public class KeyBoard : MonoBehaviour
{
    [SerializeField]
    private string Keycode1;    //normale Key-Aktion
    [SerializeField]
    private string Keycode2;    //shift Key-Aktion
    [SerializeField]
    private string Keycode3;    //alt gr Key-Aktion
    private static bool shift;  //shift pressed
    private static bool altGr;  //altgr pressed
    [SerializeField]
    private TMP_Text text1;     //normale Key-Aktion Tastatur-Text
    [SerializeField]
    private TMP_Text text2;     //shift Key-Aktion Tastatur-Text
    [SerializeField]
    private TMP_Text text3;     //alt gr Key-Aktion Tastatur-Text

    float sinceLastClick;       //Zeit in Sekunden seit letztem Klick auf die Taste
    bool pressed = false;       //Ob Key aktuell gedrücked ist
    Renderer rend;              //Renderer des Key-Cubes
    GameObject physicalKey;     //Eindrückbarer Key (ohne den Collider)
    AudioSource click;          //Klick Geräusch
    private void Start()
    {
        //Startwerte werden gesetzt
        shift = false;
        rend = gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>();
        physicalKey = gameObject.transform.GetChild(0).gameObject;
        click = gameObject.GetComponent<AudioSource>();

        //Angezeigter Text wird pro Taste gesetzt
        text1.text = Keycode1;
        text2.text = Keycode2;
        text3.text = Keycode3;
    }

    //Wird Aufgerufen, sobald der Collider des Keys auslöst
    private void OnTriggerEnter(Collider other)
    {
        //Überprüft Häufigkeit der Klicks um versehentliches Mehrfachauslösen zu verhindern
        if (Time.time - sinceLastClick > 0.05f)
        {
            //Löst nur aus mit den Fingerspitzen
            if (other.name.Equals("TargetTransform"))
            {
                //Fragt erst Spezialtasten ab (Tasten die gedrückt bleiben können)
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
                        //Fragt Spezialtasten ab (Tasten die nicht nur den String ihres Namens ausgeben)
                        if (Keycode1.Equals("Del"))
                        {
                            ScreenController.DeleteKey();
                        }
                        else if (Keycode1.Equals("Enter"))
                        {
                            ScreenController.PrintKey("\n");
                        }
                        //Fragt zu nutzende Key-Aktion ab
                        else if (altGr)
                        {
                            //Nutzt Shift-Key-Aktion wenn vorhanden
                            if (Keycode3.Length > 0)
                            {
                                ScreenController.PrintKey(Keycode3);
                            }
                        }
                        else if (shift)
                        {
                            //Nutzt Shift-Key-Aktion wenn vorhanden, ansonsten Standard Aktion
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
                //setzt neue Zeit fürs Pausieren des Klicks
                sinceLastClick = Time.time;
                //drückt Key nach unten
                physicalKey.transform.localPosition = new Vector3(0, -0.128f, 0);
                //spielt Sound
                click.Stop();
                click.Play();
            }
        }
    }
    //Wird Aufgerufen, sobald der Collider des Keys auslöst
    private void OnTriggerExit(Collider other)
    {
        //Löst nur aus mit den Fingerspitzen
        if (other.name.Equals("TargetTransform"))
        {
            pressed = false;
            //Fragt erst Spezialtasten ab (Tasten die gedrückt bleiben können)
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
            //setzt Position wieder auf den Ursprung
            physicalKey.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
