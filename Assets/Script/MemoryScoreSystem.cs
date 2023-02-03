using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoryScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI p1ScoreText, p2ScoreText;
    private bool showed = false;
    [SerializeField] private int p1Score = 0, p2Score = 0;
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
            GameObject finishScreen = Instantiate(finishScreenPrefab, GameObject.Find("Canvas").transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
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
            finishScreenSystem.ShowScreen();
            showed = true;
        }
        
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
