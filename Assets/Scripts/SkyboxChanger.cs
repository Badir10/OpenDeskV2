using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public static Material[] skyBox;
    public static GameObject[] umgebung;
    public Material[] skyBoxP;
    public GameObject[] umgebungP;

    private static int i = 0;
    private static int j = 1;

    private static float timePassed = 0;
    private static bool pause = false;
    public float maxTime;

    void Start()
    {
        skyBox = skyBoxP;
        umgebung = umgebungP;
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

    public static void SkyboxUp()
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

    public static void SkyboxDown()
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

    public static void UmgebungUp()
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
