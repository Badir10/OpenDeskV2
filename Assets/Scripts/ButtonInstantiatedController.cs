using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ButtonInstantiatedController : MonoBehaviour
{
    private bool pauseBool;
    private float timePassed = 0;
    private float maxTime = 0.5f;

    public float lerpTimer = 0.5f;
    private SkyboxChanger skyboxChanger;
    private Renderer rend;

    private MusicPlayer musicPlayer;

    void Awake()
    {
        skyboxChanger = GameObject.Find("Skybox").GetComponent<SkyboxChanger>();
        rend = gameObject.GetComponent<Renderer>();
        musicPlayer = GameObject.Find("Audio").GetComponent<MusicPlayer>();
    }

    private void Start()
    {
        pauseBool = false;
    }

    private void FixedUpdate()
    {
        if (pauseBool)
        {
            timePassed = timePassed + Time.deltaTime;
            if (timePassed >= maxTime)
            {
                timePassed = 0;
                pauseBool = false;
            }
        }
    }

    public void SkyboxUp()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            skyboxChanger.SkyboxUp();
        }
    }
    
    public void SkyboxDown()
    {
        if (!pauseBool)
        {
            skyboxChanger.SkyboxDown();
            pauseBool = true;
        }
    }
    
    public void UmgebungUp()
    {
        if (!pauseBool)
        {
            skyboxChanger.UmgebungUp();
            pauseBool = true;
        }
    }

    public void MusicUp()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            musicPlayer.MusicUp();
        }
    }

    public void MusicDown()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            musicPlayer.MusicDown();
        }
    }

    public void MusicPlayPause()
    {
        if (!pauseBool)
        {
            pauseBool = true;
            musicPlayer.MusicPlayPause();
        }
    }

    public void ColorLerpOn()
    {
        rend.material.color = Color.Lerp(Color.white, Color.green, lerpTimer);
    }
}
