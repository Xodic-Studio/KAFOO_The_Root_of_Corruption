using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public class RappitPressGameManager : MonoBehaviour
{
    public int level = 1;
    [SerializeField] private PressSystem p1PressSystem;
    [SerializeField] private PressSystem p2PressSystem;
    [SerializeField] private TimeSystem timeSystem;
    
    [Title("Finish Screen")]
    [SerializeField] private GameObject finishScreenPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] bool showed = false;

    private readonly Level _lv1 = new() {pressGoal = 100, keyLimit = 3, timeLimit = 30};
    private readonly Level _lv2 = new() {pressGoal = 120, keyLimit = 2, timeLimit = 45};
    private readonly Level _lv3 = new() {pressGoal = 150, keyLimit = 0, timeLimit = 60};
    private readonly Level _lv4 = new() {pressGoal = 200, keyLimit = 0, timeLimit = 90};
    
    public GameObject popUpPrefab;
    private PopUpSystem popUpSystem;
    public TutorialImage tutorialImageSO;
    private bool popUped;
    public int popUpImageIndex = 0;
    public Animator[] transitions;

    private void Start()
    {
        MasterScript.Instance.isInGame = true;
        level = MasterScript.Instance.minigamePlayCount[2];
        popUpImageIndex = level - 1;
        popUpSystem = Instantiate(popUpPrefab, canvas.transform).GetComponent<PopUpSystem>();
        popUpSystem.imageIndex = popUpImageIndex;
        popUpSystem.tutorialImageSO = tutorialImageSO;
        popUpSystem.ShowPopUp();
        switch (level)
        {
            case 1:
            {
                p1PressSystem.pressGoal = _lv1.pressGoal;
                p1PressSystem.SetKeyLimit(_lv1.keyLimit);
                p2PressSystem.pressGoal = _lv1.pressGoal;
                p2PressSystem.SetKeyLimit(_lv1.keyLimit);
                timeSystem.timeSpan = _lv1.timeLimit;
                break;
            }
            case 2:
            {
                p1PressSystem.pressGoal = _lv2.pressGoal;
                p1PressSystem.SetKeyLimit(_lv2.keyLimit);
                p2PressSystem.pressGoal = _lv2.pressGoal;
                p2PressSystem.SetKeyLimit(_lv2.keyLimit);
                timeSystem.timeSpan = _lv2.timeLimit;
                break;
            }
            case 3:
            {
                p1PressSystem.pressGoal = _lv3.pressGoal;
                p1PressSystem.SetKeyLimit(_lv3.keyLimit);
                p2PressSystem.pressGoal = _lv3.pressGoal;
                p2PressSystem.SetKeyLimit(_lv3.keyLimit);
                timeSystem.timeSpan = _lv3.timeLimit;
                break;
            }
            case 4:
            {
                p1PressSystem.pressGoal = _lv4.pressGoal;
                p1PressSystem.SetKeyLimit(_lv4.keyLimit);
                p2PressSystem.pressGoal = _lv4.pressGoal;
                p2PressSystem.SetKeyLimit(_lv4.keyLimit);
                timeSystem.timeSpan = _lv4.timeLimit;
                break;
            }
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl)) && !popUped)
        {
            popUped = true;
            popUpSystem.ClosePopUp(timeSystem);
        }
        if (timeSystem.timeLeft <= 0 || p1PressSystem.pressCount >= p1PressSystem.pressGoal ||
            p2PressSystem.pressCount >= p2PressSystem.pressGoal)
        {
            CheckWin();
        }
        
    }

    private void CheckWin()
    {
        if (!showed)
        {
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            // End game condition
            // P1 win
            if (p1PressSystem.pressCount > p2PressSystem.pressCount)
            {
                MasterScript.Instance.AddScore(Player.Player1);
            }
            // P2 win
            else if (p2PressSystem.pressCount > p1PressSystem.pressCount)
            {
                MasterScript.Instance.AddScore(Player.Player2);
            }
            // No one win
            else
            {
                MasterScript.Instance.AddScore(Player.Player1);
                MasterScript.Instance.AddScore(Player.Player2);
            }
            
            finishScreenSystem.ShowScreen();
            showed = true;
            MasterScript.Instance.minigamePlayCount[2]++;
            Invoke(nameof(ToSelectScene),5f);
        }
    }

    void ToSelectScene()
    {
        foreach (var VARIABLE in transitions)
        {
            VARIABLE.SetTrigger("Exit");
        }
        LoadSceneManager.Instance.LoadScene(SceneName.Selection);
    }
}

    
class Level {
    public int pressGoal;
    public int keyLimit;
    public int timeLimit;
}
