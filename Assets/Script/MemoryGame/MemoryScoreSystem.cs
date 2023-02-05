using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class MemoryScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI p1ScoreText, p2ScoreText;
    private bool showed = false;
    [SerializeField] private int p1Score = 0, p2Score = 0;
    [SerializeField] private Canvas canvas;
    public TimeSystem timeSystem;
    public GameObject finishScreenPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (timeSystem.timeLeft <= 0)
        {
            CheckCondition();
        }
    }

    void CheckCondition()
    {
        if (!showed)
        {
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            if (p1Score > p2Score)
            {
                MasterScript.Instance.p1Score++;
            }
            else if (p1Score < p2Score)
            {
                MasterScript.Instance.p2Score++;
            }
            else if (p1Score == p2Score)
            {
                MasterScript.Instance.p1Score++;
                MasterScript.Instance.p2Score++;
            }
            finishScreenSystem.ShowScreen();
            showed = true;
            MasterScript.Instance.minigamePlayCount[0]++;
            Invoke(nameof(ToSelectScene),5);
        }
        
    }
    
    void ToSelectScene()
    {
        LoadSceneManager.Instance.LoadScene(SceneName.Selection);
    }

    public void IncreaseScore(int playerNum, int increment)
    {
        if (playerNum == 1)
        {
            p1Score += increment;
            p1ScoreText.text = p1Score.ToString();
        }
        else
        {
            p2Score += increment;
            p2ScoreText.text = p2Score.ToString();
        }
    }
}
