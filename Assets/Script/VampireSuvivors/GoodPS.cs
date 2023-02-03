using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPS : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float fireRate;
    private float nextFire;
    private Vector3 bulletPosition;
    public int hp;

    void Start()
    {
        nextFire = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bulletPosition = transform.right * bulletOffset;

        RotateViewFollowMousePosition();
        Fire(bullet);
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

        if ((Input.GetAxisRaw("Fire1") != 0) && (Time.time > nextFire))
        {
            Instantiate(bullet, transform.position + bulletPosition, transform.rotation);
            nextFire = Time.time + fireRate;
        }
    }
}
