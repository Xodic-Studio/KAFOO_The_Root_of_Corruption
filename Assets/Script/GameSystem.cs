using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystem : MonoBehaviour
{
    public int level = 1;
    [SerializeField] private PressSystem P1PressSystem;
    [SerializeField] private PressSystem P2PressSystem;
    [SerializeField] private TimeSystem TimeSystem;
    
    private readonly Level _lv1 = new() {pressGoal = 150, keyLimit = 3, timeLimit = 30};
    private readonly Level _lv2 = new() {pressGoal = 150, keyLimit = 2, timeLimit = 45};
    private readonly Level _lv3 = new() {pressGoal = 175, keyLimit = 0, timeLimit = 60};
    private readonly Level _lv4 = new() {pressGoal = 250, keyLimit = 0, timeLimit = 90};

    private void Start()
    {
        switch (level)
        {
            case 1:
            {
                P1PressSystem.pressGoal = _lv1.pressGoal;
                P1PressSystem.SetKeyLimit(_lv1.keyLimit);
                P2PressSystem.pressGoal = _lv1.pressGoal;
                P2PressSystem.SetKeyLimit(_lv1.keyLimit);
                TimeSystem.timeSpan = _lv1.timeLimit;
                break;
            }
            case 2:
            {
                P1PressSystem.pressGoal = _lv2.pressGoal;
                P1PressSystem.SetKeyLimit(_lv2.keyLimit);
                P2PressSystem.pressGoal = _lv2.pressGoal;
                P2PressSystem.SetKeyLimit(_lv2.keyLimit);
                TimeSystem.timeSpan = _lv2.timeLimit;
                break;
            }
            case 3:
            {
                P1PressSystem.pressGoal = _lv3.pressGoal;
                P1PressSystem.SetKeyLimit(_lv3.keyLimit);
                P2PressSystem.pressGoal = _lv3.pressGoal;
                P2PressSystem.SetKeyLimit(_lv3.keyLimit);
                TimeSystem.timeSpan = _lv3.timeLimit;
                break;
            }
            case 4:
            {
                P1PressSystem.pressGoal = _lv4.pressGoal;
                P1PressSystem.SetKeyLimit(_lv4.keyLimit);
                P2PressSystem.pressGoal = _lv4.pressGoal;
                P2PressSystem.SetKeyLimit(_lv4.keyLimit);
                TimeSystem.timeSpan = _lv4.timeLimit;
                break;
            }
        }
    }

    void Update()
    {
        if (TimeSystem.timeLeft <= 0)
        {
            level++;
        } 
    }
}

class Level {
    public int pressGoal;
    public int keyLimit;
    public int timeLimit;
}
