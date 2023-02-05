using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTransition : MonoBehaviour
{
    private bool playedAnimation = false;
    public Animator[] LeftRightAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LoadSceneManager.Instance.loadingScene) return;
        if (!playedAnimation)
        {
            gameObject.SetActive(true);
            foreach (Animator animator in LeftRightAnimator)
            {
                StartCoroutine(PlayTransition(animator));
            }
            playedAnimation = true;
        }

    }

    IEnumerator PlayTransition(Animator animator)
    {
        Debug.Log("LOADDD AHHHH");
        animator.SetTrigger("Enter");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Exit");
    }
}
