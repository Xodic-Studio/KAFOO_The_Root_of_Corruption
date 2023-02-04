using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Required]
    public SoundData soundData;
    
    private void Start()
    {
        SoundManager.Instance.PlayMusic(soundData.GetMusicClip("SelectMusic"));
    }
    
    public void PlaySound(string soundName)
    {
        SoundManager.Instance.PlaySound(soundData.GetSoundClip(soundName));
    }
}
