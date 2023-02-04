using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GoodPlayer player;
    public int id;
    public bool isCatched;
    
    void Start()
    {
        isCatched = false;
    }

    private void OnDestroy()
    {
        player.hp--;
    }
}
