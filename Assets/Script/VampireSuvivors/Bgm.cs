using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    [SerializeField] private SoundData soundData;
    void Start()
    {
        SoundManager.Instance.PlaySound(soundData.GetSoundClip("vampire"));
    }

}
