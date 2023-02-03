using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    public Image backdrop;
    public Image background;

    public TextMeshProUGUI playerWinText;
    public TextMeshProUGUI overallScoreText;

    public Image player1Image;
    public Image player2Image;

    public Color backdropBeginColor;
    public Color backdropEndColor;

    public Vector3 backgroundBeginPosition;
    public Vector3 backgroundEndPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerWinText.text = MasterScript.Instance.p1Score > MasterScript.Instance.p2Score ? "Player 1 Win" : "Player 2 Win";
        overallScoreText.text = MasterScript.Instance.p1Score + " : " + MasterScript.Instance.p2Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScreen()
    {
        StartCoroutine(BackdropFading());
        StartCoroutine(SlideUp());
    }

    void SetupUI()
    {
        
    }

    private IEnumerator BackdropFading()
    {
        float maxTime = 3f;
        float lerpTime = 0f;
        while (lerpTime < maxTime)
        {
            backdrop.color = Color.Lerp(backdropBeginColor, backdropEndColor, lerpTime / maxTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator SlideUp()
    {
        float maxTime = 2f;
        float lerpTime = 0f;
        while (lerpTime < maxTime)
        {
            background.rectTransform.localPosition =
                Vector3.Lerp(backgroundBeginPosition, backgroundEndPosition, lerpTime / maxTime);
                lerpTime += Time.deltaTime;
            yield return null;
        }
    }
}
