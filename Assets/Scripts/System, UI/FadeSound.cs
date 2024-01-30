using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSound : MonoBehaviour
{
    // BGM에 붙여서 사용
    private float minV = 0.0f;
    private float maxV;
    private float speed;
    private AudioSource audioSource;
    public FadeState fadeState = FadeState.FADEIN;

    private void Awake()
    {
        maxV = SaveData.SoundBGMVolume;
        speed = (maxV - minV) / 300.0f;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (fadeState == FadeState.NON)
        {
            audioSource.volume = SaveData.SoundBGMVolume;
        }
        else if (fadeState == FadeState.FADEIN)
        {
            audioSource.volume = 0.0f;
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying) return;
        //Debug.Log(audioSource.volume);
        switch (fadeState)
        {
            case FadeState.FADEIN:
                audioSource.volume += speed;
                if (audioSource.volume > maxV)
                {
                    audioSource.volume = maxV;
                    fadeState = FadeState.NON;
                }
                break;

            case FadeState.FADEOUT:
                audioSource.volume -= speed;
                if (audioSource.volume < minV)
                {
                    audioSource.volume = minV;
                    fadeState = FadeState.NON;
                }
                break;
        }
    }

    public void fadeIn()
    {
        fadeState = FadeState.FADEIN;
    }

    public void fadeOut()
    {
        fadeState = FadeState.FADEOUT;
    }

}
