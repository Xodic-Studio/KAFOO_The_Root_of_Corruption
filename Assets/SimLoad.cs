using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SimLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SoundManager.Instance.StopMusic();
        StartCoroutine(VideoCheck(GetComponent<VideoPlayer>()));
    }
    
    void LoadScene()
    {
        LoadSceneManager.Instance.LoadScene(SceneName.Selection);
    }

    IEnumerator VideoCheck(VideoPlayer videoPlayer)
    {
        yield return new WaitForSeconds(2f);
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        Invoke("LoadScene",2f);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))
        {
            LoadScene();
        }
    }
}
