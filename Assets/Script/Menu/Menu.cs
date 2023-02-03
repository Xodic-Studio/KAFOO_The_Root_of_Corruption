using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private SceneName SceneName;
    
    private void Start()
    {
        playButton.onClick.AddListener(delegate
        {
            LoadSceneManager.Instance.LoadScene(this.SceneName);
        });
    }
}
