using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    public int currentLevelNumber = 1;
    public GameObject popUpPrefab;
    public GameObject pausePrefab;
    private GameObject pauseInstance;
    private PopUpSystem popUpSystem;
    public TutorialImage tutorialImageSO;
    private bool setUpSystem = false;
    public int popUpImageIndex = 0;
    private GameObject canvas;
    private bool popUped;
    private bool paused = false;
    struct Level
    {
        public float timeSpan;
        public int minRange, maxRange;
        public bool swap;
    }

    public TimeSystem timeSystem;
    public MemoryGameSystem[] memoryGameSystem;
    private static Level level1 = new Level() { timeSpan = 45, minRange = 2, maxRange = 4, swap = false };
    private static Level level2 = new Level() { timeSpan = 60, minRange = 3, maxRange = 5, swap = false };
    private static Level level3 = new Level() { timeSpan = 60, minRange = 2, maxRange = 5, swap = true };
    private static Level level4 = new Level() { timeSpan = 90, minRange = 2, maxRange = 999, swap = true };
    private Level[] allLevels = new Level[] { level1, level2, level3, level4 };
    // Start is called before the first frame update
    void Start()
    {
        currentLevelNumber = MasterScript.Instance.minigamePlayCount[0];
        popUpImageIndex = currentLevelNumber - 1;
        canvas = GameObject.Find("Canvas");
        popUpSystem = Instantiate(popUpPrefab, canvas.transform).GetComponent<PopUpSystem>();
        popUpSystem.imageIndex = popUpImageIndex;
        popUpSystem.tutorialImageSO = tutorialImageSO;
        popUpSystem.ShowPopUp();
        pauseInstance = Instantiate(pausePrefab, canvas.transform);
        pauseInstance.SetActive(false);
        Level currentLevel = allLevels[currentLevelNumber - 1];
        timeSystem.timeSpan = currentLevel.timeSpan;
        foreach (MemoryGameSystem mgs in memoryGameSystem)
        {
            mgs.minSequenceLength = currentLevel.minRange;
            mgs.maxSequenceLength = currentLevel.maxRange;
            mgs.swapAfterSequence = currentLevel.swap;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPopUp();
        CheckForPause();
    }

    void CheckForPopUp()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl)) && !popUped)
        {
            popUped = true;
            popUpSystem.ClosePopUp(timeSystem);
            if (!setUpSystem)
            {
                foreach (MemoryGameSystem mgs in memoryGameSystem)
                {
                    mgs.SetUpSystem();
                }

                setUpSystem = true;
            }
        }
    }

    void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Debug.Log("TESSSSSTTT");
            pauseInstance.SetActive(true);
            timeSystem.gameStart = false;
            paused = true;
        }
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
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
