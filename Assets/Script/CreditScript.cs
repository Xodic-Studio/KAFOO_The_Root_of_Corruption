using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreditScript : MonoBehaviour
{
    [SerializeField] private List<KeyCode> skipKeys;
    private Animator animator;
    [Required] [SerializeField] private SoundData soundData;


    private void Start()
    {
        SoundManager.Instance.PlayMusic(soundData.GetMusicClip("Credit"));
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(skipKeys[0]) || Input.GetKeyDown(skipKeys[1]))
        {
            animator.SetBool("isSkip", true);
            LoadSceneManager.Instance.LoadScene(SceneName.Menu);
        }
        
        /* Auto Skip When End
         
        if (!AnimatorIsPlaying())
        {
            animator.SetBool("isSkip", true);
            LoadSceneManager sceneManager = new LoadSceneManager();
            sceneManager.LoadScene(SceneName.Menu);
        }
        */
    }
    
    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}
