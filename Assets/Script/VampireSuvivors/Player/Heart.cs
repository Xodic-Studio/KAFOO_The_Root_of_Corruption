using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GoodPS player;
    public bool isCatched;
    
    void Start()
    {
        isCatched = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            isCatched = true;
            other.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        player.hp--;
    }
}
