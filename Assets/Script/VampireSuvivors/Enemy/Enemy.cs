using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Enemy : MonoBehaviour
{
    public int level;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 catchingOffset;
    

    public int hp;
    public float cost;
    public bool isCatching;
    public GameObject playerHeartZone;

    private int randomIndexHeart = 0;
    private GameObject heartTarget;
    private Vector2 startPos;


    private readonly Level _lv1 = new() {hp = 1, cost = 1};
    private readonly Level _lv2 = new() {hp = 4, cost = 3};
    private readonly Level _lv3 = new() {hp = 7, cost = 5};
    private readonly Level _lv4 = new() {hp = 10, cost = 7};
    
    void Start()
    {
        switch (level)
        {
            case 1:
            {
                GetComponent<SpriteRenderer>().color = Color.yellow;
                hp = _lv1.hp;
                cost = _lv1.cost;
                break;
            }
            case 2:
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                hp = _lv2.hp;
                cost = _lv2.cost;
                break;
            }
            case 3:
            {
                GetComponent<SpriteRenderer>().color = Color.magenta;
                hp = _lv3.hp;
                cost = _lv3.cost;
                break;
            }
            case 4:
            {
                GetComponent<SpriteRenderer>().color = Color.black;
                hp = _lv4.hp;
                cost = _lv4.cost;
                break;
            }
        }
        
        startPos = transform.position;
    }
    
    void Update()
    {
        if (IsEnemyDie())
        {
            Destroy(gameObject);
        }
        
        if (isCatching == false)
        {
            EnemyMoveToPlayerHeart();
        }
        else
        {
            EnemyBackToStartPos();
        }
        
    }

    private void EnemyMoveToPlayerHeart()
    {
        if (playerHeartZone.transform.childCount <= 0)
        {
            Debug.Log("No Heart");
            return;
        }
        if (heartTarget != playerHeartZone.transform.GetChild(randomIndexHeart).gameObject)
        {
            randomIndexHeart = Random.Range(0, playerHeartZone.transform.childCount);
            heartTarget = playerHeartZone.transform.GetChild(randomIndexHeart).gameObject;
            Debug.Log("Find Target");
        }
        else
        {
            if (heartTarget.GetComponent<Heart>().isCatched)
            {
                randomIndexHeart = Random.Range(0, playerHeartZone.transform.childCount);
                heartTarget = playerHeartZone.transform.GetChild(randomIndexHeart).gameObject;
                Debug.Log("Find Target");
            }
            transform.up = heartTarget.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        }

    }

    private void EnemyBackToStartPos()
    {
       // heartTarget.transform.SetParent(transform);
        heartTarget.transform.position = (Vector2)transform.position;
        heartTarget.transform.Rotate(0,0,2);
        transform.up = startPos - (Vector2)transform.position.normalized;
        GetComponent<Rigidbody2D>().velocity = transform.up * 2 * speed;
        Invoke("DestroyObject",3);
    }
    
   
    bool IsEnemyDie()
    {
        if (hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Heart"))
        {
            isCatching = true;
            heartTarget = col.gameObject;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        Destroy(heartTarget);
    }
    
    class Level
    {
        public int hp;
        public float cost;

    }
}
