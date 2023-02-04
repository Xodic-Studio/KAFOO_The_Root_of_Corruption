using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
    [Required] [SerializeField] private TextMeshProUGUI timeText;
    [Required] [SerializeField] private TextMeshProUGUI score1Text;
    [Required] [SerializeField] private TextMeshProUGUI score2Text;

    private void Start()
    {
        for (int i = 0; i < MasterScript.Instance.minigamePlayCount.Count; i++)
        {
            MasterScript.Instance.minigamePlayCount[i] = Mathf.Clamp(MasterScript.Instance.minigamePlayCount[i], 1, 4);
        }
        MasterScript.Instance.StartGame();
        score1Text.text = $"Score 1\n{MasterScript.Instance.p1Score}";
        score2Text.text = $"Score 2\n{MasterScript.Instance.p2Score}";
    }

    private void Update()
    {
        timeText.text = $"Time Left\n{MasterScript.Instance.timeLeft:F0}";
    }
}