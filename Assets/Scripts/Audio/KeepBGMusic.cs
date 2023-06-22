using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Created by H. Lloyd 
/// For project: AT03 - Indie Game
/// References:
///  - Unity Documentation
/// </summary>

public class KeepBGMusic : MonoBehaviour
{
    public AudioClip mainAudio,
        chaseAudio;

    public Notes noteScript;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = mainAudio;
        audioSource.Play();
    }

    private void Update()
    {
        if(noteScript.endGame)
        {
            audioSource.Pause();
            audioSource.clip = chaseAudio;
            audioSource.Play(0);
        }
    }
}
