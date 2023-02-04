using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantherShooterGameManager : MonoBehaviour
{
    public int currentLevelNumber = 1;
    struct Level
    {
        public Vector3 crosshairScale;
        public float sensitivity;
        public float timeSpan;
        public float pantherSkillTime;
        public float pantherSkillCooldown;
        public bool glueMode;
    }

    public TimeSystem timeSystem;
    public PantherShooterGameSystem pantherShooterGameSystem;

    private static Level level1 = new Level() { crosshairScale = new Vector3(2, 2, 2), sensitivity = 10, timeSpan = 30, pantherSkillTime = 1, pantherSkillCooldown = 3, glueMode = false };
    private static Level level2 = new Level() { crosshairScale = new Vector3(3, 3, 3), sensitivity = 15, timeSpan = 45, pantherSkillTime = 1, pantherSkillCooldown = 3,  glueMode = false };
    private static Level level3 = new Level() { crosshairScale = new Vector3(3, 3, 3), sensitivity = 15, timeSpan = 45, pantherSkillTime = 5, pantherSkillCooldown = 10,  glueMode = false };
    private static Level level4 = new Level() { crosshairScale = new Vector3(3, 3, 3), sensitivity = 20, timeSpan = 60, pantherSkillTime = 5, pantherSkillCooldown = 10,  glueMode = true };
    private Level[] allLevels = new Level[] { level1, level2, level3, level4 };
    // Start is called before the first frame update
    void Start()
    {
        Level currentLevel = allLevels[currentLevelNumber - 1];
        timeSystem.timeSpan = currentLevel.timeSpan;
        pantherShooterGameSystem.levelNum = currentLevelNumber;
        pantherShooterGameSystem.crosshairScale = currentLevel.crosshairScale;
        pantherShooterGameSystem.hunterSensitivity = currentLevel.sensitivity;
        pantherShooterGameSystem.pantherSensitivity = currentLevel.sensitivity;
        pantherShooterGameSystem.pantherSkillActivateTime = currentLevel.pantherSkillTime;
        pantherShooterGameSystem.pantherCooldown = currentLevel.pantherSkillCooldown;
        pantherShooterGameSystem.glueMode = currentLevel.glueMode;
    }
}
