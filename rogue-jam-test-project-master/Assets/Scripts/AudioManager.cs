using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : UnitySingleton<AudioManager>
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    public int noteCrossIdx = 0;

    public void PlayNoteCrossMusic()
    {
        noteCrossIdx = (noteCrossIdx + 1) % 4;
        if (noteCrossIdx == 0)
            audioSource.clip = audioClip[1];
        else if (noteCrossIdx == 1)
            audioSource.clip = audioClip[0];

        audioSource.Play();
    }

    public void OutMusicModeReset()
    {
        if (noteCrossIdx == 3)
        {
            audioSource.Play();
        }
        noteCrossIdx = 0;
    }
}
