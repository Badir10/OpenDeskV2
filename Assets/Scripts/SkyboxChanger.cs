using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyBox;

    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyBox[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkyboxUp()
    {
        if (i == skyBox.Length)
        {
            i = 0;
        }
        i++;
        RenderSettings.skybox = skyBox[i];
        Debug.Log("Skybox nummer " + i);
    }

    public void SkyboxDown()
    {
        if (i == skyBox.Length)
        {
            i = 0;
        }
        i--;
        RenderSettings.skybox = skyBox[i];
        Debug.Log("Skybox nummer " + i);
    }
}
