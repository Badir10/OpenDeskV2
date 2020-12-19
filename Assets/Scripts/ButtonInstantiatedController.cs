using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ButtonInstantiatedController : MonoBehaviour
{
    public float lerpTimer = 0.5f;
    public SkyboxChanger skyboxChanger;
    public Renderer rend;

    public MusicPlayer musicPlayer;

    void Start()
    {
        skyboxChanger = GameObject.Find("Skybox").GetComponent<SkyboxChanger>();
        rend = gameObject.GetComponent<Renderer>();
        
        musicPlayer = GameObject.Find("Audio").GetComponent<MusicPlayer>();
    }
    
    public void SkyboxUp()
    {
        skyboxChanger.SkyboxUp();
    }
    public void SkyboxDown()
    {
        skyboxChanger.SkyboxDown();
    }
    public void UmgebungUp()
    {
        skyboxChanger.UmgebungUp();
    }

    public void MusicUp()
    {
        musicPlayer.MusicUp();
    }

    public void MusicPlayPause()
    {
        musicPlayer.MusicPlayPause();
    }

    public void ColorLerpOn()
    {
        rend.material.color = Color.Lerp(Color.white, Color.green, lerpTimer);
    }
}
