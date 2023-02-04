using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHeartZone : MonoBehaviour
{
    [SerializeField] private int heartAmout;
    [SerializeField] private float offset;
    [SerializeField] private GoodPS player;
    [SerializeField] private GameObject heart;

    void Start()
    {
        heartAmout = player.hp;
        
        CreateHeart(heartAmout);
    }

    private void Update()
    {
        
    }

    void CreateHeart(int heartAmout)
    {
        if (heartAmout % 2 == 0)
        {
            transform.position = new Vector3(transform.position.x - (offset / 2), transform.position.y);
        }
        
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Vector2 centerPosition = transform.position;
        float distance = offset;
        bool isPositiveDirection = true;
        int direction = 1;
        int itemCreated = 0;
        
        
        // middle align
        for (int i = 0; i < heartAmout; i++)
        {
            
            if (i == 0)
            {
                GameObject newHeart = Instantiate(heart, position, transform.rotation, gameObject.transform);
                newHeart.GetComponent<Heart>().player = player;
            }
            else
            {
                position = new Vector2(centerPosition.x + (distance * direction), centerPosition.y);
                Instantiate(heart, position, transform.rotation, gameObject.transform);
                isPositiveDirection = !isPositiveDirection;
                itemCreated++;
            }

            if (itemCreated != 0 && itemCreated % 2 == 0)
            {
                distance += offset;
            }
            
            if (isPositiveDirection)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
        }
    }
}
