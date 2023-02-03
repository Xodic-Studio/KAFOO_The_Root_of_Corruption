using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Player
{
    Player1,
    Player2
}

public class MasterScript : Singleton<MasterScript>
{
    public float timeLeft = 0f;
    public float timeSpan = 300f;
    public int p1Score = 0;
    public int p2Score = 0;
    public bool isGameStarted = false;

    public void Start()
    {
        ResetValues();
    }
    public void StartGame()
    {
        isGameStarted = true;
    }
    
    public void Update()
    {
        if(!isGameStarted) return;
        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Clamp(timeLeft, 0, timeSpan);
        CheckCondition();
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

    private void CheckCondition()
    {
        if (!(timeLeft <= 0)) return;
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

    private void ResetValues()
    {
        timeLeft = timeSpan;
        p1Score = 0;
        p2Score = 0;
    }
}
