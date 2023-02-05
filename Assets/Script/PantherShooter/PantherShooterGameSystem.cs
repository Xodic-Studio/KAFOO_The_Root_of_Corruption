using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PantherShooterGameSystem : MonoBehaviour
{
    public int levelNum = 1;
    public SoundData soundData;
    public GameObject panther, hunter;
    public Animator hunterAnim;
    public GameObject crosshair;
    public Sprite[] swapSprites;
    public RuntimeAnimatorController[] swapAnim;
    public GameObject finishScreenPrefab;
    public Vector3 crosshairScale;
    private GameObject canvas;
    public Animator flashImage;
    public Image[] hearts;
    [SerializeField] private int currentHp;
    private CircleCollider2D crosshairCollider;
    private BoxCollider2D pantherCollider;
    private Animator crosshairAnimator;
    public TimeSystem timeSystem;
    private bool hunterShot;
    private bool pantherUsedSkill;
    private bool swapSkill;
    public bool glueMode;
    public float[] crosshairGlueRange;
    public float[] pantherGlueRange;
    public float glueTransformDuration;
    public float glueInterval;
    private bool usedSwap;
    private bool stunned;
    private bool finishShowed = false;
    public float crosshairActivateTime = 0.5f;
    public float pantherSkillActivateTime = 0.5f;
    public float hunterCooldown = 3f;
    public float pantherCooldown = 3f;
    public float hunterStunDuration = 5f;
    public float pantherSensitivity, hunterSensitivity;
    private static readonly int Flash = Animator.StringToHash("Flash");
    [Required]
    public Animator[] transitions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUp()
    {
        if (glueMode)
        {
            InvokeRepeating("GlueMode", 2f, glueInterval);
        }
        canvas = GameObject.Find("Canvas");
        currentHp = hearts.Length;
        crosshair.transform.localScale = crosshairScale;
        crosshairCollider = crosshair.GetComponent<CircleCollider2D>();
        crosshairAnimator = crosshair.GetComponent<Animator>();
        crosshairCollider.enabled = false;
        pantherCollider = panther.GetComponent<BoxCollider2D>();
        SoundManager.Instance.PlayMusic(soundData.GetMusicClip("panther_hunt"));
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeSystem.gameStart)
        {
            return;
        }
        HunterControl();
        PantherControl();
        RestrictMovement();
        WinConditionCheck();
    }

    void HunterControl()
    {
        Vector2 translation = new Vector2();
        if (!usedSwap && !stunned)
        {
            translation = new Vector2(Input.GetAxis("Horizontal WASD") * hunterSensitivity,
                Input.GetAxis("Vertical WASD") * hunterSensitivity) * Time.deltaTime;
            
        }
        else if (usedSwap)
        {
            translation = new Vector2(Input.GetAxis("Horizontal Arrow") * pantherSensitivity,
                Input.GetAxis("Vertical Arrow") * pantherSensitivity) * Time.deltaTime;
        }
        crosshair.transform.Translate(translation);
        KeyCode activationKey = usedSwap ? KeyCode.RightControl : KeyCode.Space;
        if (Input.GetKeyDown(activationKey))
        {
            if (!hunterShot)
            {
                hunterShot = true;
                StartCoroutine(HunterShoot());
            }
            
        }
    }

    void WinConditionCheck()
    {
        if (timeSystem.timeLeft <= 0)
        {
            if (!finishShowed)
            {
                timeSystem.gameStart = false;
                GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
                FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
                if (currentHp > 0)
                {
                    MasterScript.Instance.p2Score++;
                }
                else
                {
                    MasterScript.Instance.p1Score++;
                }
                finishScreenSystem.ShowScreen();
                finishShowed = true;
                MasterScript.Instance.minigamePlayCount[3]++;
                Invoke(nameof(ToSelectScene),5);
            }
        }
    }
    
    void ToSelectScene()
    {
        foreach (var VARIABLE in transitions)
        {
            VARIABLE.SetTrigger("Exit");
        }
        SoundManager.Instance.StopMusic();
        LoadSceneManager.Instance.LoadScene(SceneName.Selection);
    }
    
    void PantherControl()
    {
        Vector2 translation;
        if (usedSwap)
        {
            translation = new Vector2(Input.GetAxis("Horizontal WASD") * hunterSensitivity,
                Input.GetAxis("Vertical WASD") * hunterSensitivity) * Time.deltaTime;
        }
        else
        {
            translation = new Vector2(Input.GetAxis("Horizontal Arrow") * pantherSensitivity,
                Input.GetAxis("Vertical Arrow") * pantherSensitivity) * Time.deltaTime;
        }
        panther.transform.Translate(translation);
        if (Input.GetKeyDown(KeyCode.RightControl) && !usedSwap)
        {
            if (!pantherUsedSkill)
            {
                pantherUsedSkill = true;
                switch (levelNum)
                {
                    case 1:
                        StartCoroutine(PantherIFrame());
                        break;
                    case 2:
                        StartCoroutine(PantherIFrame());
                        break;
                    case 3:
                        StartCoroutine(PantherSwap());
                        break;
                    case 4:
                        StartCoroutine(PantherSwap());
                        break;
                }
                
            }
            
        }
    }

    void RestrictMovement()
    {
        crosshair.transform.localPosition = new Vector3(Mathf.Clamp(crosshair.transform.localPosition.x, -8.0f, 8.0f),
            Mathf.Clamp(crosshair.transform.localPosition.y, -1.5f, 4.0f), crosshair.transform.localPosition.z);
        panther.transform.localPosition = new Vector3(Mathf.Clamp(panther.transform.localPosition.x, -8.0f, 8.0f),
            Mathf.Clamp(panther.transform.localPosition.y, -1.5f, 4.0f), panther.transform.localPosition.z);
    }

    IEnumerator HunterShoot()
    {
        hunterAnim.SetTrigger("Shoot");
        SoundManager.Instance.PlaySound(soundData.GetSoundClip("gunshot"));
        crosshairCollider.enabled = true;
        crosshairAnimator.SetTrigger("Feedback");
        yield return new WaitForSeconds(crosshairActivateTime);
        crosshairCollider.enabled = false;
        StartCoroutine(HunterCooldown());
    }
    
    IEnumerator PantherIFrame()
    {
        SpriteRenderer pantherSpriteRenderer = panther.GetComponent<SpriteRenderer>();
        Color transparent = new Color(1, 1, 1, 0.5f);
        Color originalColor = pantherSpriteRenderer.color;
        pantherCollider.enabled = false;
        pantherSpriteRenderer.color = transparent;
        yield return new WaitForSeconds(pantherSkillActivateTime);
        pantherCollider.enabled = true;
        pantherSpriteRenderer.color = originalColor;
        StartCoroutine(PantherCooldown());
    }

    IEnumerator PantherSwap()
    {
        usedSwap = true;
        flashImage.SetTrigger(Flash);
        SpriteRenderer hunterSpriteRenderer = hunter.GetComponent<SpriteRenderer>();
        SpriteRenderer pantherSpriteRenderer = panther.GetComponent<SpriteRenderer>();
        Animator hunterAnimator = hunter.GetComponent<Animator>();
        Animator pantherAnimator = panther.GetComponent<Animator>();
        Sprite originalHunter = hunterSpriteRenderer.sprite;
        Sprite originalPanther = pantherSpriteRenderer.sprite;
        RuntimeAnimatorController originalHunterAC = hunterAnimator.runtimeAnimatorController;
        RuntimeAnimatorController originalPantherAC = pantherAnimator.runtimeAnimatorController;
        hunterSpriteRenderer.sprite = swapSprites[0];
        pantherSpriteRenderer.sprite = swapSprites[1];
        hunterAnimator.runtimeAnimatorController = swapAnim[0];
        pantherAnimator.runtimeAnimatorController = swapAnim[1];
        yield return new WaitForSeconds(pantherSkillActivateTime);
        flashImage.SetTrigger(Flash);
        hunterSpriteRenderer.sprite = originalHunter;
        pantherSpriteRenderer.sprite = originalPanther;
        hunterAnimator.runtimeAnimatorController = originalHunterAC;
        pantherAnimator.runtimeAnimatorController = originalPantherAC;
        usedSwap = false;
        StartCoroutine(PantherCooldown());
    }

    void GlueMode()
    {
        StartCoroutine(ChangeScale());
    }

    IEnumerator ChangeScale()
    {
        Vector3 crosshairGlueScale = new Vector3(Random.Range(crosshairGlueRange[0], crosshairGlueRange[1]),
            Random.Range(crosshairGlueRange[0], crosshairGlueRange[1]),
            Random.Range(crosshairGlueRange[0], crosshairGlueRange[1]));
        Vector3 pantherGlueScale = new Vector3(Random.Range(pantherGlueRange[0], pantherGlueRange[1]),
            Random.Range(pantherGlueRange[0], pantherGlueRange[1]),
            Random.Range(pantherGlueRange[0], pantherGlueRange[1]));
        float maxTime = glueTransformDuration;
        float lerpTime = 0f;
        while (lerpTime < maxTime)
        {
            crosshair.transform.localScale =
                Vector3.Lerp(crosshair.transform.localScale, crosshairGlueScale, lerpTime / maxTime);
            panther.transform.localScale =
                Vector3.Lerp(panther.transform.localScale, pantherGlueScale, lerpTime / maxTime);
            lerpTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator HunterCooldown()
    {
        float timer = hunterCooldown;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        hunterShot = false;
        yield return null;
    }
    
    IEnumerator PantherCooldown()
    {
        float timer = pantherCooldown;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        pantherUsedSkill = false;
        yield return null;
    }

    IEnumerator HunterStunned()
    {
        while (usedSwap)
        {
            yield return null;
        }
        stunned = true;
        float timer = hunterStunDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        stunned = false;
        yield return null;
    }

    public void Hit()
    {
        if (usedSwap)
        {
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("foo_hurt"));
            StartCoroutine(HunterStunned());
        }
        else
        {
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("panther_hurt"));
            currentHp -= 1;
            currentHp = Mathf.Clamp(currentHp, 0, hearts.Length);
            StartCoroutine(HitFeedback());
            hearts[currentHp].enabled = false;
        }

        if (currentHp <= 0)
        {
            SoundManager.Instance.PlaySound(soundData.GetSoundClip("panther_die"));
            timeSystem.gameStart = false;
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            MasterScript.Instance.p1Score++;
            finishScreenSystem.ShowScreen();
            finishShowed = true;
            MasterScript.Instance.minigamePlayCount[3]++;
            Invoke(nameof(ToSelectScene),5);
        }
    }

    IEnumerator HitFeedback()
    {
        panther.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        panther.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
