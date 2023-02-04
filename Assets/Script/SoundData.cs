using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    public AudioClip[] musicClips;
    public AudioClip[] soundClips;
    
    /// <summary>
    /// Get music clip by name
    /// </summary>
    /// <param name="clipName">File Name</param>
    /// <returns></returns>
    public AudioClip GetMusicClip(string clipName)
    {
        foreach (var clip in musicClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }
    
    /// <summary>
    /// Get sound clip by name
    /// </summary>
    /// <param name="clipName">File Name</param>
    /// <returns></returns>
    public AudioClip GetSoundClip(string clipName)
    {
        foreach (var clip in soundClips)
        {
            if (clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }
}
