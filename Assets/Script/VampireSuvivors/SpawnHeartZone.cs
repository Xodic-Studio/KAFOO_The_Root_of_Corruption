using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    void Update()
    {
        
    }

    void CreateHeart(int heartAmout)
    {
        Vector2 offsetVector2 = new Vector2(transform.position.x, transform.position.y);
        
        for (int i = 0; i < heartAmout; i++)
        {
            Instantiate(heart, offsetVector2, transform.rotation, gameObject.transform);
            offsetVector2 += new Vector2(offset, 0);
        }
    }
}
