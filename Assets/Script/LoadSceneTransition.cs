using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTransition : MonoBehaviour
{
    private bool exited = false;
    public Animator[] LeftRightAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        /* For transition scope back
        if (!LoadSceneManager.Instance.loadingScene)
        {
            return;
        }

        if (!exited)
        {
            gameObject.SetActive(true);
            foreach (Animator animator in LeftRightAnimator)
            {
                StartCoroutine(PlayTransition(animator));
            }
            exited = true;
        }
        */
    }

    IEnumerator PlayTransition(Animator animator)
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Exit");
    }
}
