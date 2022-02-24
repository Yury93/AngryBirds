using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    public void PlayAudio(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();
    }

}
