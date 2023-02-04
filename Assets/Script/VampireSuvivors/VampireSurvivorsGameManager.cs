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
    [SerializeField] private TimeSystem timeSystem;
    [SerializeField] private GameObject finishScreenPrefab;
    [SerializeField] private bool finishShowed;
    public int dificultLevel;
    public GameObject popUpPrefab;
    private PopUpSystem popUpSystem;
    public TutorialImage tutorialImageSO;
    private bool setUpSystem = false;
    public int popUpImageIndex = 0;
    private GameObject canvas;
    private bool popUped;



    private void Start()
    {
        MasterScript.Instance.isInGame = true;
        dificultLevel = MasterScript.Instance.minigamePlayCount[1];
        popUpImageIndex = dificultLevel - 1;
        canvas = GameObject.Find("Canvas");
        popUpSystem = Instantiate(popUpPrefab, canvas.transform).GetComponent<PopUpSystem>();
        popUpSystem.imageIndex = popUpImageIndex;
        popUpSystem.tutorialImageSO = tutorialImageSO;
        popUpSystem.ShowPopUp();
        
        spawnEnemyZone.AssignData(gameSystemData[dificultLevel - 1]);
        goodPlayer.AssignData(gameSystemData[dificultLevel - 1]);
        time.timeSpan = gameSystemData[dificultLevel - 1].timeSpan;
    }
    
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl)) && !popUped)
        {
            popUped = true;
            popUpSystem.ClosePopUp(timeSystem);
            if (!setUpSystem)
            {
                goodPlayer.SetUp();
                spawnEnemyZone.SetUp();
                setUpSystem = true;
            }
        }

        if (!finishShowed)
        {
            checkWin();
        }
    }

    
    private void checkWin()
    {
        // one win
        if (time.timeLeft <= 0 && goodPlayer.hp <= 0)
        {
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            MasterScript.Instance.p1Score++;
            MasterScript.Instance.p2Score++;
            finishScreenSystem.ShowScreen();
            finishShowed = true;
            MasterScript.Instance.minigamePlayCount[1]++;
            Invoke(nameof(ToSelectScene),5);
        }
        // bad player win
        else if (goodPlayer.hp <= 0)
        {
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            MasterScript.Instance.p1Score++;
            finishScreenSystem.ShowScreen();
            finishShowed = true;
            MasterScript.Instance.minigamePlayCount[1]++;
            Invoke(nameof(ToSelectScene),5);
        }
        // good player win
        else if (time.timeLeft <= 0)
        {
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            MasterScript.Instance.p2Score++;
            finishScreenSystem.ShowScreen();
            finishShowed = true;
            MasterScript.Instance.minigamePlayCount[1]++;
            Invoke(nameof(ToSelectScene),5);
        }
    }
    
    void ToSelectScene()
    {
        LoadSceneManager.Instance.LoadScene(SceneName.Selection);
    }
}
