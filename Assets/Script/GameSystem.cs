using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    [HideInInspector] public int level = 1;
    [SerializeField] public PressSystem PressSystem;
    [SerializeField] private ProgressBar ProgressBar;
    [SerializeField] private TimeSystem TimeSystem;
    
    void Update()
    {
        if (TimeSystem.timeLeft <= 0)
        {
            level++;
        } 
    }
}
