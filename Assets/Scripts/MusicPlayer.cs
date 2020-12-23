using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    ///// In dieser Klasse befinden sich alle methoden die fuer das Abspielen und Pausieren der Musik notwendig sind /////
    ///
    
    [SerializeField]
    private AudioClip[] audioClips;
    
    private bool isPlaying;
    private AudioSource _audioSource;
    private int currentMusic = 0;
    
    void Awake()
    {
        // Die AudioSource Komponente wird zu beginn des Scripts gespeichert
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Die Musik wird zu Beginn abgespielt und der Play-Status wird mit true gespeichert
        _audioSource.Play();
        isPlaying = true;
    }


    public void MusicPlayPause()
    {
        // Wenn die Musik gerade nicht abgespielt wird soll die AudioSource wieder abgespielt werden und der Status auch wieder auf true gesetzt werden
        if (!isPlaying)
        {
            _audioSource.UnPause();
            isPlaying = true;
        }
        
        // Wenn die Musik gerade abgespielt wird soll Pausiert werden und der Status geaendert werden
        else
        {
            _audioSource.Pause();
            isPlaying = false;
        }
    }

    public void MusicUp()
    {
        // Wenn die gerade Abspielt wird soll der Clip auf den naechsthöheren umgeaendert werden 
        if (isPlaying)
        {
            // Wenn der Clip beim ende des Arrays ankommt soll wieder der erste Clip abgespielt werden
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
    
    // wie Musikup nur anders herum
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
