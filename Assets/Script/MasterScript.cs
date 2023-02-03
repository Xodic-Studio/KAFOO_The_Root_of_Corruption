using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Player
{
    Player1,
    Player2
}

public class MasterScript : MonoBehaviour
{
    public float timeLeft = 0f;
    public float timeSpan = 300f;
    public int p1Score = 0;
    public int p2Score = 0;
    public bool isGameStarted = false;

    public void Start()
    {
        timeLeft = timeSpan;
    }
    
    public void Update()
    {
        if(!isGameStarted) return;
        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Clamp(timeLeft, 0, timeSpan);
    }
    
    public void AddScore(Player player)
    {
        if (player == Player.Player1)
        {
            p1Score ++;
        }
        else
        {
            p2Score ++;
        }
    }
    
    public void CheckCondition()
    {
        if (timeLeft <= 0)
        {
            if (p1Score > p2Score)
            {
                Debug.Log("P1 Win");
            }
            else if (p1Score < p2Score)
            {
                Debug.Log("P2 Win");
            }
            else if (p1Score == p2Score)
            {
                Debug.Log("Draw!!!");
            }

            if (isGameStarted) isGameStarted = false;
            ResetValues();
        }
    }

    public void ResetValues()
    {
        timeLeft = timeSpan;
        p1Score = 0;
        p2Score = 0;
    }
}
