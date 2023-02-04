using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VampireSurvivorsGameManager : MonoBehaviour
{
    [SerializeField] private SpawnEnemyZone spawnEnemyZone;
    [SerializeField] private GoodPlayer goodPlayer;
    [SerializeField] private TimeSystem time;
    [SerializeField] private List<GameSystemData> gameSystemData;
    public int dificultLevel;



    private void Start()
    {
        switch (dificultLevel)
        {
            case 1:
            {
                spawnEnemyZone.AssignData(gameSystemData[0]);
                goodPlayer.AssignData(gameSystemData[0]);
                time.timeSpan = gameSystemData[0].timeSpan;
                break;
            }
            case 2:
            {
                spawnEnemyZone.AssignData(gameSystemData[1]);
                goodPlayer.AssignData(gameSystemData[1]);
                time.timeSpan = gameSystemData[1].timeSpan;
                break;
            }
            case 3:
            {
                spawnEnemyZone.AssignData(gameSystemData[2]);
                goodPlayer.AssignData(gameSystemData[2]);
                time.timeSpan = gameSystemData[2].timeSpan;
                break;
                
            }
            case 4:
            {
                spawnEnemyZone.AssignData(gameSystemData[3]);
                goodPlayer.AssignData(gameSystemData[3]);
                time.timeSpan = gameSystemData[3].timeSpan;
                break;
            }
        }
    }
    
    private void Update()
    {
  
        checkWin();
    }

    
    private void checkWin()
    {
        // one win
        if (time.timeLeft <= 0 && goodPlayer.hp <= 0)
        {
            Debug.Log("Win Win");
        }
        // bad player win
        else if (goodPlayer.hp <= 0)
        {
            Debug.Log("Bad Win");
        }
        // good player win
        else if (time.GetComponent<TimeSystem>().timeLeft <= 0)
        {
            Debug.Log("Good Win");
        }
    }
}
