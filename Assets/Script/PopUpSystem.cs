using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{
    [HideInInspector] public int imageIndex;
    [HideInInspector] public TutorialImage tutorialImageSO;
    public Image backdrop;
    public Image background;
    public Image popUpImage;
    public Color backdropBeginColor;
    public Color backdropEndColor;
    public Vector3 backgroundBeginPosition;
    public Vector3 backgroundEndPosition;
    // Start is called before the first frame update
    void Start()
    {
        popUpImage.sprite = tutorialImageSO.tutorialImages[imageIndex];
    }

    // Update is called once per frame

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
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
