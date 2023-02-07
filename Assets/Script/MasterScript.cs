using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum Player
{
    Player1,
    Player2
}

public class MasterScript : Singleton<MasterScript>
{
    public float timeLeft = 0f;
    public float timeSpan = 1000f;
    public int p1Score = 0;
    public int p2Score = 0;
    public int winCondition;
    public bool isGameStarted = false;
    public bool isInGame = false;
    public bool isEnded = false;
    public List<int> minigamePlayCount = new List<int>() {1, 1, 1, 1};

    public void Start()
    {
        ResetValues();
    }
    public void StartGame()
    {
        LoadSceneManager.Instance.loadingScene = false;
        LoadSceneManager.Instance.finishedLoading = true;
        isGameStarted = true;
        isInGame = false;
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
        if (isInGame) return;
        if (isEnded) return;
        if (p1Score > p2Score)
        {
            isEnded = true;
            Debug.Log("P1 Win");
            winCondition = 0;
            SoundManager.Instance.StopMusic();
            LoadSceneManager.Instance.LoadScene(SceneName.EndSceneBad);
        }
        else if (p1Score < p2Score)
        {
            isEnded = true;
            Debug.Log("P2 Win");
            winCondition = 1;
            SoundManager.Instance.StopMusic();
            LoadSceneManager.Instance.LoadScene(SceneName.EndSceneGood);
        }
        else if (p1Score == p2Score)
        {
            isEnded = true;
            Debug.Log("Draw!!!");
            winCondition = 2;
            SoundManager.Instance.StopMusic();
            LoadSceneManager.Instance.LoadScene(SceneName.EndSceneBad);
        }
        
        Debug.Log($"P1 : {p1Score} / P2 : {p2Score}");
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
