using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    
    public AudioClip[] audioClips;
    private bool isPlaying;
    private AudioSource _audioSource;
    private int i;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    
    public void MusicPlayPause()
    {
        if (!isPlaying)
        {
            _audioSource.Play();
            isPlaying = true;
        }
        if (isPlaying)
        {
            _audioSource.Pause();
            isPlaying = false;
        }
    }

    public void MusicUp()
    {
        if (isPlaying)
        {
            if (i > audioClips.Length)
            {
                i = 0;
            }

            i++;
            _audioSource.clip = audioClips[i];
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }
}
