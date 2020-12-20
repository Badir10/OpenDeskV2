using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugOculus : MonoBehaviour
{
    public static DebugOculus Instance;
    bool inMenu;
    Text logText;
    

    void Awake(){
        Instance = this;
    }
    void Start()
    {
        var rt = DebugUIBuilder.instance.AddLabel("Debug");
        logText = rt.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two))
        {
            if(inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }
    public void Log(string msg){
        logText.text = msg;
    }
}
