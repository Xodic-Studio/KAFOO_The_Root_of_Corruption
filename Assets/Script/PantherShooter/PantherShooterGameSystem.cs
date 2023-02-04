using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PantherShooterGameSystem : MonoBehaviour
{
    public int levelNum = 1;
    public GameObject panther, hunter;
    public GameObject crosshair;
    public GameObject finishScreenPrefab;
    public Vector3 crosshairScale;
    private GameObject canvas;
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

    // Start is called before the first frame update
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
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
                Invoke(nameof(ToSelectScene),5);
            }
        }
    }
    
    void ToSelectScene()
    {
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
            Mathf.Clamp(crosshair.transform.localPosition.y, -2.5f, 4.0f), crosshair.transform.localPosition.z);
        panther.transform.localPosition = new Vector3(Mathf.Clamp(panther.transform.localPosition.x, -8.0f, 8.0f),
            Mathf.Clamp(panther.transform.localPosition.y, -2.5f, 4.0f), panther.transform.localPosition.z);
    }

    IEnumerator HunterShoot()
    {
        crosshairCollider.enabled = true;
        crosshairAnimator.SetTrigger("Feedback");
        yield return new WaitForSeconds(crosshairActivateTime);
        crosshairCollider.enabled = false;
        StartCoroutine(HunterCooldown());
    }
    
    IEnumerator PantherIFrame()
    {
        pantherCollider.enabled = false;
        yield return new WaitForSeconds(pantherSkillActivateTime);
        pantherCollider.enabled = true;
        StartCoroutine(PantherCooldown());
    }

    IEnumerator PantherSwap()
    {
        usedSwap = true;
        yield return new WaitForSeconds(pantherSkillActivateTime);
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
            StartCoroutine(HunterStunned());
        }
        else
        {
            currentHp -= 1;
            currentHp = Mathf.Clamp(currentHp, 0, hearts.Length);
            hearts[currentHp].enabled = false;
        }

        if (currentHp <= 0)
        {
            GameObject finishScreen = Instantiate(finishScreenPrefab, canvas.transform);
            FinishScreen finishScreenSystem = finishScreen.GetComponent<FinishScreen>();
            MasterScript.Instance.p1Score++;
            finishScreenSystem.ShowScreen();
            finishShowed = true;
            Invoke(nameof(ToSelectScene),5);
        }
    }
}
