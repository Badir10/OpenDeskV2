﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
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



    public void SkyboxUp()
    {
        currentSkybox++;
        if (currentSkybox == skyBox.Length)
        {
            currentSkybox = 0;
        }
        RenderSettings.skybox = skyBox[currentSkybox];
    }

    public void SkyboxDown()
    {
        currentSkybox--;
        if (currentSkybox < 0)
        {
            currentSkybox = skyBox.Length-1;
        }
        RenderSettings.skybox = skyBox[currentSkybox];
        
    }

    public void UmgebungUp()
    {
        int oldScene = currentUmgebung;
        currentUmgebung++;

        if (currentUmgebung == surrounding.Length)
        {
            currentUmgebung = 0;
        }
        surrounding[oldScene].SetActive(false);
        surrounding[currentUmgebung].SetActive(true);
    }
}
