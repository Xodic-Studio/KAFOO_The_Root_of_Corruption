using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    [HideInInspector] public int imageIndex;
    [HideInInspector] public TutorialImage tutorialImageSO;
    public Image backdrop;
    public Image background;
    public Image popUpImage;
    public TextMeshProUGUI countDownText;
    private Image[] allElements;
    public Color backdropBeginColor;
    public Color backdropEndColor;
    public Vector3 backgroundBeginPosition;
    public Vector3 backgroundEndPosition;
    // Start is called before the first frame update
    void Start()
    {
        countDownText.gameObject.SetActive(false);
        allElements = new[] { backdrop, background, popUpImage };
        popUpImage.sprite = tutorialImageSO.tutorialImages[imageIndex];
    }

    // Update is called once per frame

    public void ClosePopUp(TimeSystem timeSystem)
    {
        StartCoroutine(FadeOut(timeSystem));
    }
    
    public void ShowPopUp()
    {
        StartCoroutine(BackdropFading());
        StartCoroutine(SlideUp());
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
        float maxTime = 1f;
        float lerpTime = 0f;
        while (lerpTime < maxTime)
        {
            background.rectTransform.localPosition =
                Vector3.Lerp(backgroundBeginPosition, backgroundEndPosition, lerpTime / maxTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOut(TimeSystem timeSystem)
    {
        backdrop.color = backdropEndColor;
        background.rectTransform.localPosition = backgroundEndPosition;
        float maxTime = 1.5f;
        float lerpTime = 0f;
        Color transparent = new Color(1, 1, 1, 0);
        while (lerpTime < maxTime)
        {
            foreach (Image image in allElements)
            {
                image.color = Color.Lerp(image.color, transparent, lerpTime / maxTime);
            }
            lerpTime += Time.deltaTime;
            yield return null;
        }
        countDownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        timeSystem.gameStart = true;
        yield return null;
    }
}
