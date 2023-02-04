using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    private bool paused = false;
    public GameObject pausePrefab;
    private GameObject pauseInstance;
    public TimeSystem timeSystem;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        pauseInstance = Instantiate(pausePrefab, canvas.transform);
        pauseInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause();
    }
    
    void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused && timeSystem.gameStart)
        {
            pauseInstance.SetActive(true);
            timeSystem.gameStart = false;
            Time.timeScale = 0;
            paused = true;
            return;
        }
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                pauseInstance.SetActive(false);
                timeSystem.gameStart = true;
                paused = false;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl)))
            {
                LoadSceneManager.Instance.LoadScene(SceneName.Selection);
            }
        }
    }
}
