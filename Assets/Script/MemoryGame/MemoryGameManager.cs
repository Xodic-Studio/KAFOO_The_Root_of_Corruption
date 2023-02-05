using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    public int currentLevelNumber = 1;
    public GameObject popUpPrefab;
    private PopUpSystem popUpSystem;
    public TutorialImage tutorialImageSO;
    private bool setUpSystem = false;
    public int popUpImageIndex = 0;
    private GameObject canvas;
    private bool popUped;
   
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
    [Required]
    public SoundData soundData;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(soundData.GetMusicClip("bgm"));
        MasterScript.Instance.isInGame = true;
        currentLevelNumber = MasterScript.Instance.minigamePlayCount[0];
        popUpImageIndex = currentLevelNumber - 1;
        canvas = GameObject.Find("Canvas");
        popUpSystem = Instantiate(popUpPrefab, canvas.transform).GetComponent<PopUpSystem>();
        popUpSystem.imageIndex = popUpImageIndex;
        popUpSystem.tutorialImageSO = tutorialImageSO;
        popUpSystem.ShowPopUp();
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
}
