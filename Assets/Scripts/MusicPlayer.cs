using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    
    public AudioClip[] audioClips;
    private bool isPlaying;
    private AudioSource _audioSource;
    private int currentMusic = 0;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.Play();
        isPlaying = true;
    }


    public void MusicPlayPause()
    {
        if (!isPlaying)
        {
            _audioSource.UnPause();
            isPlaying = true;
        }
        else if (isPlaying)
        {
            _audioSource.Pause();
            isPlaying = false;
        }
    }

    public void MusicUp()
    {
        if (isPlaying)
        {
            if (currentMusic == audioClips.Length)
            {
                currentMusic = 0;
            }

            currentMusic++;
            _audioSource.clip = audioClips[currentMusic];
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }
    
    public void MusicDown()
    {
        if (isPlaying)
        {
            if (currentMusic == 0)
            {
                currentMusic = audioClips.Length;
            }
            
            currentMusic--;
            _audioSource.clip = audioClips[currentMusic];
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }
}
