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

    private Vector3 bulletPosition;
    private float nextFire;
    
    public float staminaRegenRate;
    public int hp;
    [SerializeField] private float maxStamina = 10f;
    public float stamina;

    public void AssignData(GameSystemData gameSystemData)
    {
        hp = gameSystemData.goodPlayerHp;
        stamina = gameSystemData.goodPlayerStaminaRegen;
    }
    void Start()
    {
        nextFire = 0;
        InvokeRepeating(nameof(StaminaRegen), 0, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
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
