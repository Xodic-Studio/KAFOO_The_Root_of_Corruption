using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HeartZone : MonoBehaviour
{
    private int heartAmout;
    [SerializeField] private float offset;
    [SerializeField] private GoodPlayer player;
    [SerializeField] private GameObject heartPrefab;

    void Start()
    {
        heartAmout = player.hp;
        
        CreateHeart(heartAmout);
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
                GameObject newHeart = Instantiate(heartPrefab, position, transform.rotation, gameObject.transform);
                newHeart.GetComponent<Heart>().player = player;
            }
            else
            {
                position = new Vector2(centerPosition.x + (distance * direction), centerPosition.y);
                Instantiate(heartPrefab, position, transform.rotation, gameObject.transform);
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
            AssignIDToHeart();
        }
    }
    
    void AssignIDToHeart()
    {
        foreach (var heart in transform.GetComponentsInChildren<Heart>())
        {
            heart.id = heart.transform.GetSiblingIndex();
            heart.player = player;
        }
    }
}
