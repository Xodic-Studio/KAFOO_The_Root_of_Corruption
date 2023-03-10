using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class GoodPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float fireRate;
    [SerializeField] private Scrollbar staminaBar;
    [SerializeField] private TimeSystem timeSystem;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shoot;
 
    private Vector3 bulletPosition;
    private float nextFire;
    
    public float staminaRegenRate;
    public int hp;
    
    [SerializeField] private float maxStamina = 10f;
    public float stamina;

    public void AssignData(GameSystemData gameSystemData)
    {
        hp = gameSystemData.goodPlayerHp;
        staminaRegenRate = gameSystemData.goodPlayerStaminaRegen;
        fireRate = gameSystemData.goodPlayerFireRate;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetUp()
    {
        nextFire = 0;
        InvokeRepeating(nameof(StaminaRegen), 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeSystem.gameStart) return;
        bulletPosition = transform.right* bulletOffset;
        RotateViewFollowMousePosition();
        Fire(bullet);
        StaminaBarUpdate();
    }

    public void RotateViewFollowMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.right = direction;
    }

    public void Fire(GameObject bullet)
    {

        if ((Input.GetAxisRaw("Fire1") != 0) && (Time.time > nextFire) && stamina > 0)
        {
            audioSource.clip = shoot;
            audioSource.Play();
            Instantiate(bullet, transform.position + bulletPosition, transform.rotation);
            nextFire = Time.time + fireRate;
            stamina--;
        }
    }

    public void StaminaBarUpdate()
    {
        staminaBar.size = stamina / maxStamina;
    }
    
    void StaminaRegen()
    {
        stamina += staminaRegenRate;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
