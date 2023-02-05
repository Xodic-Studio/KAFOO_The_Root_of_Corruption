using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;

    [SerializeField] private List<Sprite> bulletPic;
    public int bulletDmg = 1;
    private Rigidbody2D rb;
  
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = bulletPic[Random.Range(0, bulletPic.Count)]; 
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyBullet(3));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    IEnumerator DestroyBullet(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    
}
