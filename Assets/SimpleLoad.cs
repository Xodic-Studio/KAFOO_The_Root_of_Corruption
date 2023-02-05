using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleLoad : MonoBehaviour
{
    [Required] [SerializeField] SoundData soundData;
    private void Start()
    {
        SoundManager.Instance.PlaySound(soundData.GetSoundClip("WinSound"));
        Invoke("LoadScene",5f);
    }
    
    void LoadScene()
    {
        LoadSceneManager.Instance.LoadScene(SceneName.Credit);
    }
}
