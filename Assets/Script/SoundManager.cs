using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    
    public void PlayMusic(AudioClip audioClip)
    {
        Debug.Log(audioClip.name);
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
    
    public void PauseMusic()
    {
        audioSource.Pause();
    }
    
    public void ResumeMusic()
    {
        audioSource.UnPause();
    }
    
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }



}
