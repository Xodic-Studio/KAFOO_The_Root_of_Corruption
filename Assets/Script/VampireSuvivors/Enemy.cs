using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private float speed;

    public int hp;
    public int cost;

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
                hp = _lv1.hp;
                cost = _lv1.cost;
                break;
            }
            case 2:
            {
                hp = _lv2.hp;
                cost = _lv2.cost;
                break;
            }
            case 3:
            {
                hp = _lv3.hp;
                cost = _lv3.cost;
                break;
            }
            case 4:
            {
                hp = _lv4.hp;
                cost = _lv4.cost;
                break;
            }
        }
    }
    
    void Update()
    {
        if (IsEnemyDie())
        {
            Destroy(gameObject);
        }
        //EnemyMoveToPlayer();
    }

   /* void EnemyMoveToPlayer()
    {
        transform.up = playerTarget.transform.position - transform.position;
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
    }
    */
   
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

    class Level
    {
        public int hp;
        public int cost;

    }
}
