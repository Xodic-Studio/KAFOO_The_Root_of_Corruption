using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{

    public int currentLevelNumber = 1;
    struct Level
    {
        public float timeSpan;
        public int minRange, maxRange;
        public bool swap;
    }

    public TimeSystem timeSystem;
    public MemoryGameSystem[] memoryGameSystem;

    private static Level level1 = new Level() { timeSpan = 5, minRange = 2, maxRange = 4, swap = false };
    private static Level level2 = new Level() { timeSpan = 60, minRange = 3, maxRange = 5, swap = false };
    private static Level level3 = new Level() { timeSpan = 60, minRange = 2, maxRange = 5, swap = true };
    private static Level level4 = new Level() { timeSpan = 90, minRange = 2, maxRange = 999, swap = true };
    private Level[] allLevels = new Level[] { level1, level2, level3, level4 };
    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}
