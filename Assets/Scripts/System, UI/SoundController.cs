using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public bool isBGM = true;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int clipCount;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        clipCount = audioClips.Length;
    }


    private void Start()
    {
        if (isBGM)
        {
            //audioSource.volume = SaveData.SoundBGMVolume;
        }
        else
        {
            audioSource.volume = SaveData.SoundSEVolume;
        }
    }

    public void updateVolume()
    {
        if (isBGM)
        {
            audioSource.volume = SaveData.SoundBGMVolume;
        }
        else
        {
            audioSource.volume = SaveData.SoundSEVolume;
        }
        //Debug.Log(audioSource.volume);
    }

    public bool isPlaying()
    {
        if (audioSource.isPlaying) return true;

        return false;
    }

    // 효과음 용 메소드들 
    public void playClip()
    {
        if (isBGM) return;
        if (clipCount == 0)
        {
            audioSource.Play();
            return;
        }
        audioSource.Stop();
        int num = Random.Range(0, clipCount);
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }

    public void playClipWithStartTime(float time)
    {
        if (isBGM) return;
        if (clipCount == 0)
        {
            audioSource.time = time;
            audioSource.Play();
            return;
        }
        audioSource.Stop();
        int num = Random.Range(0, clipCount);
        audioSource.time = time;
        audioSource.clip = audioClips[num];
        audioSource.Play();
    }


}
