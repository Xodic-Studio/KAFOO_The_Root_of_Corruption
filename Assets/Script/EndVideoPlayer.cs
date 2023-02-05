using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideoPlayer : MonoBehaviour
{
    public VideoData videoData;
    // Start is called before the first frame update
    void Start()
    {
        VideoClip[] videoClips = videoData.videoClips;
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = videoClips[MasterScript.Instance.winCondition];
        videoPlayer.loopPointReached += VideoEnd;
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void VideoEnd(VideoPlayer vp)
    {
        LoadSceneManager.Instance.LoadScene(SceneName.Credit);
    }
}
