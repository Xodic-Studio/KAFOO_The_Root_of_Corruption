using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantherShooterGameSystem : MonoBehaviour
{
    public int levelNum = 1;
    public GameObject panther, hunter;
    public GameObject crosshair;
    public Vector3 crosshairScale;
    public Image[] hearts;
    [SerializeField] private int currentHp;
    private CircleCollider2D crosshairCollider;
    private BoxCollider2D pantherCollider;
    private Animator crosshairAnimator;
    private bool hunterShot;
    private bool pantherUsedSkill;
    private bool swapSkill;
    private bool usedSwap;
    private bool stunned;
    public float crosshairActivateTime = 0.5f;
    public float pantherSkillActivateTime = 0.5f;
    public float hunterCooldown = 3f;
    public float pantherCooldown = 3f;
    public float hunterStunDuration = 5f;
    public float pantherSensitivity, hunterSensitivity;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    void HunterControl()
    {
        Vector2 translation;
        if (!usedSwap && !stunned)
        {
            translation = new Vector2(Input.GetAxis("Horizontal WASD") * hunterSensitivity,
                Input.GetAxis("Vertical WASD") * hunterSensitivity) * Time.deltaTime;
            
        }
        else
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
    
    void PantherControl()
    {
        Vector2 translation;
        if (usedSwap)
        {
            Debug.Log("SWAPPPP");
            translation = new Vector2(Input.GetAxis("Horizontal WASD") * hunterSensitivity,
                Input.GetAxis("Vertical WASD") * hunterSensitivity) * Time.deltaTime;
            
        }
        else
        {
            Debug.Log("NOOO SWAPPPP");
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
                        break;
                }
                
            }
            
        }
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
    }
}
