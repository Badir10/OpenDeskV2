using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    ///// In dieser Klasse befinden sich alle methoden die fuer das Abspielen und Pausieren der Musik notwendig sind /////
    /// 

    public Material[] skyBox;
    public GameObject[] surrounding;

    private int currentSkybox;
    private int currentUmgebung;
    
    void Start()
    {
        currentSkybox = 0;
        currentUmgebung = 0;
        
        RenderSettings.skybox = skyBox[currentSkybox];
        surrounding[currentUmgebung].SetActive(true);
    }



    // Waehlt die naechste Skybox aus
    public void SkyboxUp()
    {
        if (currentSkybox == skyBox.Length)
        {
            currentSkybox = 0;
        }
            
        currentSkybox++;
        RenderSettings.skybox = skyBox[currentSkybox];
    }

    // Waehlt die vorherige Skybox aus
    public void SkyboxDown()
    {
        if (currentSkybox == 0)
        {
            currentSkybox = skyBox.Length;
        }
        currentSkybox--;
        RenderSettings.skybox = skyBox[currentSkybox];
    }

    // Waehlt die naechsthoehere Umgebung aus
    public void UmgebungUp()
    {
        if (currentUmgebung == surrounding.Length)
        {
            currentUmgebung = 0;
        }
        surrounding[currentUmgebung].SetActive(false);
        currentUmgebung++;
        surrounding[currentUmgebung].SetActive(true);
    }
    
    
}
