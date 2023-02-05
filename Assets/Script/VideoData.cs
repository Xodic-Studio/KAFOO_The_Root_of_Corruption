using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Ending Video", menuName = "ScriptableObject/Ending Video")]
public class VideoData : ScriptableObject
{
    public VideoClip[] videoClips;
}
