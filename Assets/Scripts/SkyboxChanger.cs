using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyBox;
    public GameObject[] umgebung;

    private int i = 0;
    private int j = 1;

    private float timePassed = 0;
    private bool pause = false;
    public float maxTime;

    void Start()
    {
        RenderSettings.skybox = skyBox[0];
        umgebung[0].SetActive(true);
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

    public void SkyboxUp()
    {
        if (!pause)
        {
            pause = true;
            if (i == skyBox.Length)
            {
                i = 0;
            }
            i++;
            RenderSettings.skybox = skyBox[i];
            Debug.Log("Skybox nummer " + i); 
        }
    }

    public void SkyboxDown()
    {
        if (!pause)
        {
            pause = true;
            if (i == skyBox.Length)
            {
                i = 0;
            }
            i--;
            RenderSettings.skybox = skyBox[i];
            Debug.Log("Skybox nummer " + i);
        }
    }

    public void UmgebungUp()
    {
        if (!pause)
        {
            pause = true;
            if (j == umgebung.Length)
            {
                j = 0;
            }
            umgebung[j].SetActive(false);
            j++;
            umgebung[j].SetActive(true);
        }
    }
}
